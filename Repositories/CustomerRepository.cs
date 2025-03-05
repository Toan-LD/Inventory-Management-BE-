using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
	{
		public CustomerRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
