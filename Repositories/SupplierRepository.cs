using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
	{
		public SupplierRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
