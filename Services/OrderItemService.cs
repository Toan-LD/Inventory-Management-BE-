using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class OrderItemService
	{
		private readonly IUnitOfWork _unitOfWork;

		public OrderItemService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
		{
			var orderItems = await _unitOfWork.OrderItem.GetAllAsync();
			return orderItems ?? new List<OrderItem>();
		}

		public async Task<OrderItem> GetOrderItemByIdAsync(int id)
		{
			try
			{
				var orderItem = await _unitOfWork.OrderItem.GetByIdAsync(id);
				return orderItem;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy chi tiết đơn hàng: {ex.Message}");
				throw;
			}
		}

		public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
		{
			try
			{
				if (orderItem == null)
				{
					throw new ArgumentNullException(nameof(orderItem), "Chi tiết đơn hàng không được null");
				}
				await _unitOfWork.OrderItem.CreatedAsync(orderItem);
				await _unitOfWork.SaveChangesAsync();
				return orderItem;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm chi tiết đơn hàng: {ex.Message}");
				throw;
			}
		}

		public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
		{
			try
			{
				if (orderItem == null)
				{
					throw new ArgumentNullException(nameof(orderItem), "Chi tiết đơn hàng không được null");
				}
				var existingOrderItem = await _unitOfWork.OrderItem.GetByIdAsync(orderItem.OrderItemId);
				if (existingOrderItem == null)
				{
					throw new ArgumentException($"Không tìm thấy chi tiết đơn hàng với ID: {orderItem.OrderItemId}");
				}
				await _unitOfWork.OrderItem.UpdateAsync(orderItem);
				await _unitOfWork.SaveChangesAsync();
				return orderItem;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật chi tiết đơn hàng: {ex.Message}");
				throw;
			}
		}
	}
}
