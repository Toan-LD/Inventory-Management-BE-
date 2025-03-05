using InventoryManagement.Data;
using InventoryManagement.Interfaces;

namespace InventoryManagement.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly InventoryManagementDbContext _context;

		public UnitOfWork(
			InventoryManagementDbContext context,
			IItemRepository itemRepository,
			ISupplierRepository supplierRepository,
			ICustomerRepository customerRepository,
			ILocationRepository locationRepository,
			IOrderRepository orderRepository,
			IOrderItemRepository orderItemRepository,
			IStockRepository stockRepository,
			ITransactionRepository transactionRepository,
			IUserRepository userRepository,
			IWarehouseTaskRepository warehouseTaskRepository)
		{
			_context = context;
			Item = itemRepository;
			Supplier = supplierRepository;
			Customer = customerRepository;
			Location = locationRepository;
			Order = orderRepository;
			OrderItem = orderItemRepository;
			Stock = stockRepository;
			Transaction = transactionRepository;
			User = userRepository;
			WarehouseTask = warehouseTaskRepository;
		}

		public IItemRepository Item { get; set; }
		public ISupplierRepository Supplier { get; set; }
		public ICustomerRepository Customer { get; set; }
		public ILocationRepository Location { get; set; }
		public IOrderRepository Order { get; set; }
		public IOrderItemRepository OrderItem { get; set; }
		public IStockRepository Stock { get; set; }
		public ITransactionRepository Transaction { get; set; }
		public IUserRepository User { get; set; }
		public IWarehouseTaskRepository WarehouseTask { get; set; }

		public void Dispose()
		{
			_context.Dispose();
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
