using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
	public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
	{
		public TransactionRepository(InventoryManagementDbContext context) : base(context)
		{
		}
	}
}
