using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CustomerController : ControllerBase
	{
		private readonly CustomerService _customerService;

		public CustomerController(CustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
		{
			var customers = await _customerService.GetAllCustomersAsync();
			return Ok(customers);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Customer>> GetCustomerById(int id)
		{
			var customer = await _customerService.GetCustomerByIdAsync(id);
			if (customer == null)
			{
				return NotFound($"Không tìm thấy khách hàng với ID: {id}");
			}
			return Ok(customer);
		}

		[HttpPost]
		public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
		{
			if (customer == null)
			{
				return BadRequest("Khách hàng không được null");
			}
			var createdCustomer = await _customerService.AddCustomerAsync(customer);
			return CreatedAtAction(nameof(GetCustomerById), new { id = createdCustomer.CustomerId }, createdCustomer);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Customer>> UpdateCustomer(int id, Customer customer)
		{
			if (customer == null || id != customer.CustomerId)
			{
				return BadRequest("Dữ liệu khách hàng không hợp lệ");
			}
			var updatedCustomer = await _customerService.UpdateCustomerAsync(customer);
			return Ok(updatedCustomer);
		}
	}
}
