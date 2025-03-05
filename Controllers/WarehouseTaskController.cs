using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseTaskController : ControllerBase
    {
		private readonly WarehouseTaskService _warehouseTaskService;

		public WarehouseTaskController(WarehouseTaskService warehouseTaskService)
		{
			_warehouseTaskService = warehouseTaskService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<WarehouseTask>>> GetAllWarehouseTasks()
		{
			var tasks = await _warehouseTaskService.GetAllWarehouseTasksAsync();
			return Ok(tasks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<WarehouseTask>> GetWarehouseTaskById(int id)
		{
			var task = await _warehouseTaskService.GetWarehouseTaskByIdAsync(id);
			if (task == null)
			{
				return NotFound($"Không tìm thấy nhiệm vụ kho với ID: {id}");
			}
			return Ok(task);
		}

		[HttpPost]
		public async Task<ActionResult<WarehouseTask>> AddWarehouseTask(WarehouseTask task)
		{
			if (task == null)
			{
				return BadRequest("Nhiệm vụ kho không được null");
			}
			var createdTask = await _warehouseTaskService.AddWarehouseTaskAsync(task);
			return CreatedAtAction(nameof(GetWarehouseTaskById), new { id = createdTask.TaskId }, createdTask);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<WarehouseTask>> UpdateWarehouseTask(int id, WarehouseTask task)
		{
			if (task == null || id != task.TaskId)
			{
				return BadRequest("Dữ liệu nhiệm vụ kho không hợp lệ");
			}
			var updatedTask = await _warehouseTaskService.UpdateWarehouseTaskAsync(task);
			return Ok(updatedTask);
		}
	}
}
