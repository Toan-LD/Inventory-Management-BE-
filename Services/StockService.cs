using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class StockService
	{
		private readonly IUnitOfWork _unitOfWork;

		public StockService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Stock>> GetAllStocksAsync()
		{
			var stocks = await _unitOfWork.Stock.GetAllAsync();
			return stocks ?? new List<Stock>();
		}

		public async Task<Stock> GetStockByIdAsync(int id)
		{
			try
			{
				var stock = await _unitOfWork.Stock.GetByIdAsync(id);
				return stock;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy tồn kho: {ex.Message}");
				throw;
			}
		}

		public async Task<Stock> AddStockAsync(Stock stock)
		{
			try
			{
				if (stock == null)
				{
					throw new ArgumentNullException(nameof(stock), "Tồn kho không được null");
				}
				await _unitOfWork.Stock.CreatedAsync(stock);
				await _unitOfWork.SaveChangesAsync();
				return stock;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm tồn kho: {ex.Message}");
				throw;
			}
		}

		public async Task<Stock> UpdateStockAsync(Stock stock)
		{
			try
			{
				if (stock == null)
				{
					throw new ArgumentNullException(nameof(stock), "Tồn kho không được null");
				}
				var existingStock = await _unitOfWork.Stock.GetByIdAsync(stock.StockId);
				if (existingStock == null)
				{
					throw new ArgumentException($"Không tìm thấy tồn kho với ID: {stock.StockId}");
				}
				await _unitOfWork.Stock.UpdateAsync(stock);
				await _unitOfWork.SaveChangesAsync();
				return stock;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật tồn kho: {ex.Message}");
				throw;
			}
		}
	}
}
