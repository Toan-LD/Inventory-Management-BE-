using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class LocationRepository : GenericRepository<Location>, ILocationRepository
	{
		public LocationRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
