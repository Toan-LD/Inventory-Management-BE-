using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class OrderService
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Order>> GetAllOrdersAsync()
		{
			var orders = await _unitOfWork.Order.GetAllAsync();
			return orders ?? new List<Order>();
		}

		public async Task<Order> GetOrderByIdAsync(int id)
		{
			try
			{
				var order = await _unitOfWork.Order.GetByIdAsync(id);
				return order;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy đơn hàng: {ex.Message}");
				throw;
			}
		}

		public async Task<Order> AddOrderAsync(Order order)
		{
			try
			{
				if (order == null)
				{
					throw new ArgumentNullException(nameof(order), "Đơn hàng không được null");
				}
				await _unitOfWork.Order.CreatedAsync(order);
				await _unitOfWork.SaveChangesAsync();
				return order;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm đơn hàng: {ex.Message}");
				throw;
			}
		}

		public async Task<Order> UpdateOrderAsync(Order order)
		{
			try
			{
				if (order == null)
				{
					throw new ArgumentNullException(nameof(order), "Đơn hàng không được null");
				}
				var existingOrder = await _unitOfWork.Order.GetByIdAsync(order.OrderId);
				if (existingOrder == null)
				{
					throw new ArgumentException($"Không tìm thấy đơn hàng với ID: {order.OrderId}");
				}
				await _unitOfWork.Order.UpdateAsync(order);
				await _unitOfWork.SaveChangesAsync();
				return order;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật đơn hàng: {ex.Message}");
				throw;
			}
		}
	}
}
