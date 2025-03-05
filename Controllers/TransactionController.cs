using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
		private readonly TransactionService _transactionService;

		public TransactionController(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
		{
			var transactions = await _transactionService.GetAllTransactionsAsync();
			return Ok(transactions);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Transaction>> GetTransactionById(int id)
		{
			var transaction = await _transactionService.GetTransactionByIdAsync(id);
			if (transaction == null)
			{
				return NotFound($"Không tìm thấy giao dịch với ID: {id}");
			}
			return Ok(transaction);
		}

		[HttpPost]
		public async Task<ActionResult<Transaction>> AddTransaction(Transaction transaction)
		{
			if (transaction == null)
			{
				return BadRequest("Giao dịch không được null");
			}
			var createdTransaction = await _transactionService.AddTransactionAsync(transaction);
			return CreatedAtAction(nameof(GetTransactionById), new { id = createdTransaction.TransactionId }, createdTransaction);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Transaction>> UpdateTransaction(int id, Transaction transaction)
		{
			if (transaction == null || id != transaction.TransactionId)
			{
				return BadRequest("Dữ liệu giao dịch không hợp lệ");
			}
			var updatedTransaction = await _transactionService.UpdateTransactionAsync(transaction);
			return Ok(updatedTransaction);
		}
	}
}
