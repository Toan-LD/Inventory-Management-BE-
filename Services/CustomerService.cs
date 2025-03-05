using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class CustomerService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CustomerService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
		{
			var customers = await _unitOfWork.Customer.GetAllAsync();
			return customers ?? new List<Customer>();
		}

		public async Task<Customer> GetCustomerByIdAsync(int id)
		{
			try
			{
				var customer = await _unitOfWork.Customer.GetByIdAsync(id);
				return customer;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy khách hàng: {ex.Message}");
				throw;
			}
		}

		public async Task<Customer> AddCustomerAsync(Customer customer)
		{
			try
			{
				if (customer == null)
				{
					throw new ArgumentNullException(nameof(customer), "Khách hàng không được null");
				}
				await _unitOfWork.Customer.CreatedAsync(customer);
				await _unitOfWork.SaveChangesAsync();
				return customer;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm khách hàng: {ex.Message}");
				throw;
			}
		}

		public async Task<Customer> UpdateCustomerAsync(Customer customer)
		{
			try
			{
				if (customer == null)
				{
					throw new ArgumentNullException(nameof(customer), "Khách hàng không được null");
				}
				var existingCustomer = await _unitOfWork.Customer.GetByIdAsync(customer.CustomerId);
				if (existingCustomer == null)
				{
					throw new ArgumentException($"Không tìm thấy khách hàng với ID: {customer.CustomerId}");
				}
				await _unitOfWork.Customer.UpdateAsync(customer);
				await _unitOfWork.SaveChangesAsync();
				return customer;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật khách hàng: {ex.Message}");
				throw;
			}
		}
	}
}
