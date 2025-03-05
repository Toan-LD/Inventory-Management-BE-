using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class UserController : ControllerBase
    {
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
		{
			var users = await _userService.GetAllUsersAsync();
			return Ok(users);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUserById(int id)
		{
			var user = await _userService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound($"Không tìm thấy người dùng với ID: {id}");
			}
			return Ok(user);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult<User>> AddUser(User user)
		{
			if (user == null)
			{
				return BadRequest("Người dùng không được null");
			}
			var createdUser = await _userService.AddUserAsync(user);
			return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<User>> UpdateUser(int id, User user)
		{
			if (user == null || id != user.UserId)
			{
				return BadRequest("Dữ liệu người dùng không hợp lệ");
			}
			var updatedUser = await _userService.UpdateUserAsync(user);
			return Ok(updatedUser);
		}
	}
}
