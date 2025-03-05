using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class SupplierService
	{
		private readonly IUnitOfWork _unitOfWork;

		public SupplierService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// Lấy tất cả nhà cung cấp
		public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
		{
			var suppliers = await _unitOfWork.Supplier.GetAllAsync();
			return suppliers ?? new List<Supplier>();
		}

		// Lấy nhà cung cấp theo ID
		public async Task<Supplier> GetSupplierByIdAsync(int id)
		{
			try
			{
				var supplier = await _unitOfWork.Supplier.GetByIdAsync(id);
				return supplier;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy nhà cung cấp: {ex.Message}");
				throw;
			}
		}

		// Thêm nhà cung cấp mới
		public async Task<Supplier> AddSupplierAsync(Supplier supplier)
		{
			try
			{
				if (supplier == null)
				{
					throw new ArgumentNullException(nameof(supplier), "Nhà cung cấp không được null");
				}

				await _unitOfWork.Supplier.CreatedAsync(supplier);
				return supplier;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm nhà cung cấp: {ex.Message}");
				throw;
			}
		}

		// Cập nhật nhà cung cấp
		public async Task<Supplier> UpdateSupplierAsync(Supplier supplier)
		{
			try
			{
				if (supplier == null)
				{
					throw new ArgumentNullException(nameof(supplier), "Nhà cung cấp không được null");
				}

				var existingSupplier = await _unitOfWork.Supplier.GetByIdAsync(supplier.SupplierId);
				if (existingSupplier == null)
				{
					throw new ArgumentException($"Không tìm thấy nhà cung cấp với ID: {supplier.SupplierId}");
				}

				await _unitOfWork.Supplier.UpdateAsync(supplier);
				return supplier;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật nhà cung cấp: {ex.Message}");
				throw;
			}
		}

		// Xóa nhà cung cấp
		public async Task<bool> DeleteSupplierAsync(int id)
		{
			try
			{
				var supplier = await _unitOfWork.Supplier.GetByIdAsync(id);
				if (supplier == null)
				{
					return false;
				}

				await _unitOfWork.Supplier.DeleteAsync(supplier);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi xóa nhà cung cấp: {ex.Message}");
				throw;
			}
		}
	}
}
