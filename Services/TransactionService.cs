using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class TransactionService
	{
		private readonly IUnitOfWork _unitOfWork;

		public TransactionService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
		{
			var transactions = await _unitOfWork.Transaction.GetAllAsync();
			return transactions ?? new List<Transaction>();
		}

		public async Task<Transaction> GetTransactionByIdAsync(int id)
		{
			try
			{
				var transaction = await _unitOfWork.Transaction.GetByIdAsync(id);
				return transaction;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy giao dịch: {ex.Message}");
				throw;
			}
		}

		public async Task<Transaction> AddTransactionAsync(Transaction transaction)
		{
			try
			{
				if (transaction == null)
				{
					throw new ArgumentNullException(nameof(transaction), "Giao dịch không được null");
				}
				await _unitOfWork.Transaction.CreatedAsync(transaction);
				await _unitOfWork.SaveChangesAsync();
				return transaction;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm giao dịch: {ex.Message}");
				throw;
			}
		}

		public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
		{
			try
			{
				if (transaction == null)
				{
					throw new ArgumentNullException(nameof(transaction), "Giao dịch không được null");
				}
				var existingTransaction = await _unitOfWork.Transaction.GetByIdAsync(transaction.TransactionId);
				if (existingTransaction == null)
				{
					throw new ArgumentException($"Không tìm thấy giao dịch với ID: {transaction.TransactionId}");
				}
				await _unitOfWork.Transaction.UpdateAsync(transaction);
				await _unitOfWork.SaveChangesAsync();
				return transaction;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật giao dịch: {ex.Message}");
				throw;
			}
		}
	}
}
