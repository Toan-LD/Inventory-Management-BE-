using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
		private readonly SupplierService _supplierService;

		public SupplierController(SupplierService supplierService)
		{
			_supplierService = supplierService;
		}

		// GET: api/supplier
		[HttpGet]
		public async Task<IActionResult> GetAllSuppliers()
		{
			try
			{
				var suppliers = await _supplierService.GetAllSuppliersAsync();
				return Ok(suppliers);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}

		// GET: api/supplier/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetSupplierById(int id)
		{
			try
			{
				var supplier = await _supplierService.GetSupplierByIdAsync(id);
				if (supplier == null)
				{
					return NotFound($"Không tìm thấy nhà cung cấp với ID: {id}");
				}
				return Ok(supplier);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}

		// POST: api/supplier
		[HttpPost]
		public async Task<IActionResult> AddSupplier([FromBody] Supplier supplier)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var addedSupplier = await _supplierService.AddSupplierAsync(supplier);
				return CreatedAtAction(nameof(GetSupplierById), new { id = addedSupplier.SupplierId }, addedSupplier);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}

		// PUT: api/supplier/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
		{
			try
			{
				if (id != supplier.SupplierId)
				{
					return BadRequest("ID trong URL không khớp với ID của nhà cung cấp.");
				}

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var updatedSupplier = await _supplierService.UpdateSupplierAsync(supplier);
				return Ok(updatedSupplier);
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}

		// DELETE: api/supplier/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteSupplier(int id)
		{
			try
			{
				var deleted = await _supplierService.DeleteSupplierAsync(id);
				if (!deleted)
				{
					return NotFound($"Không tìm thấy nhà cung cấp với ID: {id}");
				}
				return NoContent();
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Lỗi server: {ex.Message}");
			}
		}
	}
}
