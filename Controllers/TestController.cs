using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class TestController : ControllerBase
    {
		[HttpGet]
		public IActionResult Test()
		{
			return Ok("API đang hoạt động");
		}
	}
}
