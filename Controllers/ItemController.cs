using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
		private readonly ItemService _itemService;

		public ItemController(ItemService itemService)
		{
			_itemService = itemService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
		{
			var items = await _itemService.GetAllItemAsync();
			return Ok(items);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Item>> GetItemById(int id)
		{
			var item = await _itemService.GetItemByIdAsync(id);
			if (item == null)
			{
				return NotFound($"Không tìm thấy sản phẩm với ID: {id}");
			}
			return Ok(item);
		}

		[HttpPost]
		public async Task<ActionResult<Item>> AddItem(Item item)
		{
			if (item == null)
			{
				return BadRequest("Sản phẩm không được null");
			}
			var createdProduct = await _itemService.AddItemAsync(item);
			return CreatedAtAction(nameof(GetItemById), new { id = createdProduct.ItemId }, createdProduct);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<Item>> UpdateProduct(int id, Item item)
		{
			if (item == null || id != item.ItemId)
			{
				return BadRequest("Dữ liệu sản phẩm không hợp lệ");
			}
			var updatedProduct = await _itemService.UpdateItemAsync(item);
			return Ok(updatedProduct);
		}
	}
}
