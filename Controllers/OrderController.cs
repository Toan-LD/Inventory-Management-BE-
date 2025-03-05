using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
		private readonly OrderService _orderService;

		public OrderController(OrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
		{
			var orders = await _orderService.GetAllOrdersAsync();
			return Ok(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetOrderById(int id)
		{
			var order = await _orderService.GetOrderByIdAsync(id);
			if (order == null)
			{
				return NotFound($"Không tìm thấy đơn hàng với ID: {id}");
			}
			return Ok(order);
		}

		[HttpPost]
		public async Task<ActionResult<Order>> AddOrder(Order order)
		{
			if (order == null)
			{
				return BadRequest("Đơn hàng không được null");
			}
			var createdOrder = await _orderService.AddOrderAsync(order);
			return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
		{
			if (order == null || id != order.OrderId)
			{
				return BadRequest("Dữ liệu đơn hàng không hợp lệ");
			}
			var updatedOrder = await _orderService.UpdateOrderAsync(order);
			return Ok(updatedOrder);
		}
	}
}
