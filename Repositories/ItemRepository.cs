using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class ItemRepository : GenericRepository<Item>, IItemRepository
	{
		public ItemRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
