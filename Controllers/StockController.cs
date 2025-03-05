using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
		private readonly StockService _stockService;

		public StockController(StockService stockService)
		{
			_stockService = stockService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Stock>>> GetAllStocks()
		{
			var stocks = await _stockService.GetAllStocksAsync();
			return Ok(stocks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Stock>> GetStockById(int id)
		{
			var stock = await _stockService.GetStockByIdAsync(id);
			if (stock == null)
			{
				return NotFound($"Không tìm thấy tồn kho với ID: {id}");
			}
			return Ok(stock);
		}

		[HttpPost]
		public async Task<ActionResult<Stock>> AddStock(Stock stock)
		{
			if (stock == null)
			{
				return BadRequest("Tồn kho không được null");
			}
			var createdStock = await _stockService.AddStockAsync(stock);
			return CreatedAtAction(nameof(GetStockById), new { id = createdStock.StockId }, createdStock);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Stock>> UpdateStock(int id, Stock stock)
		{
			if (stock == null || id != stock.StockId)
			{
				return BadRequest("Dữ liệu tồn kho không hợp lệ");
			}
			var updatedStock = await _stockService.UpdateStockAsync(stock);
			return Ok(updatedStock);
		}
	}
}
