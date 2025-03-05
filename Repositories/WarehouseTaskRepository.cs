using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class WarehouseTaskRepository : GenericRepository<WarehouseTask>, IWarehouseTaskRepository
	{
		public WarehouseTaskRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
