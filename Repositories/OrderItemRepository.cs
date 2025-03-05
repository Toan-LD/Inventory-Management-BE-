using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
	{
		public OrderItemRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
