using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
		private readonly OrderItemService _orderItemService;

		public OrderItemController(OrderItemService orderItemService)
		{
			_orderItemService = orderItemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
		{
			var orderItems = await _orderItemService.GetAllOrderItemsAsync();
			return Ok(orderItems);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
		{
			var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
			if (orderItem == null)
			{
				return NotFound($"Không tìm thấy chi tiết đơn hàng với ID: {id}");
			}
			return Ok(orderItem);
		}

		[HttpPost]
		public async Task<ActionResult<OrderItem>> AddOrderItem(OrderItem orderItem)
		{
			if (orderItem == null)
			{
				return BadRequest("Chi tiết đơn hàng không được null");
			}
			var createdOrderItem = await _orderItemService.AddOrderItemAsync(orderItem);
			return CreatedAtAction(nameof(GetOrderItemById), new { id = createdOrderItem.OrderItemId }, createdOrderItem);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<OrderItem>> UpdateOrderItem(int id, OrderItem orderItem)
		{
			if (orderItem == null || id != orderItem.OrderItemId)
			{
				return BadRequest("Dữ liệu chi tiết đơn hàng không hợp lệ");
			}
			var updatedOrderItem = await _orderItemService.UpdateOrderItemAsync(orderItem);
			return Ok(updatedOrderItem);
		}
	}
}
