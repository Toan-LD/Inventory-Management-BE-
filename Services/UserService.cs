using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using InventoryManagement_BE_.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagement.Services
{
	public class UserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;

		public UserService(IUnitOfWork unitOfWork, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_config = config;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			var users = await _unitOfWork.User.GetAllAsync();
			return users ?? new List<User>();
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			try
			{
				var user = await _unitOfWork.User.GetByIdAsync(id);
				return user;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy người dùng: {ex.Message}");
				throw;
			}
		}

		public async Task<User> AddUserAsync(User user)
		{
			try
			{
				if (user == null)
				{
					throw new ArgumentNullException(nameof(user), "Người dùng không được null");
				}

				user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

				await _unitOfWork.User.CreatedAsync(user);
				await _unitOfWork.SaveChangesAsync();
				return user;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm người dùng: {ex.Message}");
				throw;
			}
		}

		public async Task<User> UpdateUserAsync(User user)
		{
			try
			{
				if (user == null)
				{
					throw new ArgumentNullException(nameof(user), "Người dùng không được null");
				}
				var existingUser = await _unitOfWork.User.GetByIdAsync(user.UserId);
				if (existingUser == null)
				{
					throw new ArgumentException($"Không tìm thấy người dùng với ID: {user.UserId}");
				}

				user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

				await _unitOfWork.User.UpdateAsync(user);
				await _unitOfWork.SaveChangesAsync();
				return user;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật người dùng: {ex.Message}");
				throw;
			}
		}

		public async Task DeleteUserAsync(int id)
		{
			try
			{
				var user = await _unitOfWork.User.GetByIdAsync(id);
				if (user == null)
				{
					throw new ArgumentException($"Không tìm thấy người dùng với ID: {id}");
				}
				await _unitOfWork.User.DeleteAsync(user);
				await _unitOfWork.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi xóa người dùng: {ex.Message}");
				throw;
			}
		}

		public async Task<AuthResponseDTO> LoginAsync(LoginRequestDTO loginDTO)
		{
			try
			{
				var users = await _unitOfWork.User.GetAllAsync();

				var user = users.FirstOrDefault(u => u.Username == loginDTO.Username && BCrypt.Net.BCrypt.Verify(loginDTO.Password, u.PasswordHash));

				if (user is null)
					return null;

				var token = GenerateJwtToken(user, loginDTO.RememberMe);
				var refreshToken = GenerateRefreshToken();
				var expiryMinutes = loginDTO.RememberMe ? 1440 : int.Parse(_config["Jwt:ExpiryInMinutes"]);

				user.RefreshToken = refreshToken;
				user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpiryInDays"]));
				await _unitOfWork.User.UpdateAsync(user);
				await _unitOfWork.SaveChangesAsync();
				return new AuthResponseDTO
				{
					Token = token,
					RefreshToken = refreshToken,
					ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes)
				};

			} catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi đăng nhập: {ex.Message}");
				throw;
			}
		}

		public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequestDTO rftDTO)
		{
			try
			{
				var users = await _unitOfWork.User.GetAllAsync();
				var user = users.FirstOrDefault(u => u.RefreshToken == rftDTO.RefreshToken);
				if(user is null || user.RefreshTokenExpiry < DateTime.UtcNow)
					throw new UnauthorizedAccessException("Token không hợp lệ");

				var token = GenerateJwtToken(user, false); //luôn là 1h khi refresh
				var expiryMinutes = int.Parse(_config["Jwt:ExpiryInMinutes"]);

				//Gia hạn thêm refresh thêm 14 ngày kể từ ngày refresh
				user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(int.Parse(_config["Jwt:RefreshTokenExpiryInDays"]));
				await _unitOfWork.User.UpdateAsync(user);
				await _unitOfWork.SaveChangesAsync();

				return new AuthResponseDTO
				{
					Token = token,
					RefreshToken = user.RefreshToken,
					ExpiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes)
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi làm mới token: {ex.Message}");
				throw;
			}
		}

		public async Task LogoutAsync(string refreshToken)
		{
			try
			{
				var users = await _unitOfWork.User.GetAllAsync();
				var user = users.FirstOrDefault(u => u.RefreshToken == refreshToken);

				if (user is not null)
				{
					user.RefreshToken = null;
					user.RefreshTokenExpiry = null;
					await _unitOfWork.User.UpdateAsync(user);
					await _unitOfWork.SaveChangesAsync();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi đăng xuất: {ex.Message}");
				throw;
			}
		}



		private string GenerateRefreshToken()
		{
			return Guid.NewGuid().ToString();
		}

		private string GenerateJwtToken(User user, bool rememberMe)
		{
			var jwtSettings = _config.GetSection("Jwt");
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //Định nghĩa thuật toán ký (HmacSha256) cho token.

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var exipryInMinutes = rememberMe ? int.Parse(jwtSettings["RememberMeExpiryInMinutes"]) : int.Parse(jwtSettings["ExpiryInMinutes"]);
			var token = new JwtSecurityToken(
				issuer: jwtSettings["Issuer"],
				audience: jwtSettings["Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(exipryInMinutes),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
