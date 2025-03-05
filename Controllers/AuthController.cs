using InventoryManagement.Services;
using InventoryManagement_BE_.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_BE_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(UserService userService)
		{
			_userService = userService;
		}

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] LoginRequestDTO loginDTO)
        {
            if(loginDTO is null || string.IsNullOrEmpty(loginDTO.Username) || string.IsNullOrEmpty(loginDTO.Password))
			{
				return BadRequest("Thông tin đăng nhập không hợp lệ");
			}

            var response = await _userService.LoginAsync(loginDTO);
			return response is null ? NotFound("Thông tin người dùng không đúng") : Ok(response);
		}

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponseDTO>> RefreshToken([FromBody] RefreshTokenRequestDTO rftDTO)
        {
            if(rftDTO is null || string.IsNullOrEmpty(rftDTO.RefreshToken))
                return BadRequest("Token không hợp lệ");

            var response = await _userService.RefreshTokenAsync(rftDTO);
            return Ok(response);
		}

		[HttpPost("logout")]
		[Authorize]
		public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDTO rftDTO)
		{
			if (rftDTO == null || string.IsNullOrEmpty(rftDTO.RefreshToken))
			{
				return BadRequest("Refresh token không hợp lệ");
			}

			await _userService.LogoutAsync(rftDTO.RefreshToken);
			return NoContent();
		}
	}
}
