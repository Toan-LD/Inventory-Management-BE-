using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
		private readonly LocationService _locationService;

		public LocationController(LocationService locationService)
		{
			_locationService = locationService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Location>>> GetAllLocations()
		{
			var locations = await _locationService.GetAllLocationsAsync();
			return Ok(locations);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Location>> GetLocationById(int id)
		{
			var location = await _locationService.GetLocationByIdAsync(id);
			if (location == null)
			{
				return NotFound($"Không tìm thấy vị trí với ID: {id}");
			}
			return Ok(location);
		}

		[HttpPost]
		public async Task<ActionResult<Location>> AddLocation(Location location)
		{
			if (location == null)
			{
				return BadRequest("Vị trí không được null");
			}
			var createdLocation = await _locationService.AddLocationAsync(location);
			return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.LocationId }, createdLocation);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Location>> UpdateLocation(int id, Location location)
		{
			if (location == null || id != location.LocationId)
			{
				return BadRequest("Dữ liệu vị trí không hợp lệ");
			}
			var updatedLocation = await _locationService.UpdateLocationAsync(location);
			return Ok(updatedLocation);
		}
	}
}
