namespace InventoryManagement.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IItemRepository Item { get; }
		ISupplierRepository Supplier { get; }
		ICustomerRepository Customer { get; }
		ILocationRepository Location { get; }
		IOrderRepository Order { get; }
		IOrderItemRepository OrderItem { get; }
		IStockRepository Stock { get; }
		ITransactionRepository Transaction { get; }
		IUserRepository User { get; }
		IWarehouseTaskRepository WarehouseTask { get; }


		Task<int> SaveChangesAsync();
	}
}
