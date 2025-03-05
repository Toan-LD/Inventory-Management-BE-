using InventoryManagement.Interfaces;
using InventoryManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryManagement.Services
{
	public class ItemService
	{
		private readonly IUnitOfWork _unitOfWork;

		public ItemService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Item>> GetAllItemAsync()
		{
			var items = await _unitOfWork.Item.GetAllAsync();
			return items ?? new List<Item>();
		}

		public async Task<Item> GetItemByIdAsync(int id)
		{
			try
			{
				var items = await _unitOfWork.Item.GetByIdAsync(id);
				return items;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy sản phẩm: {ex.Message}");
				throw;
			}
		}

		public async Task<Item> AddItemAsync(Item item)
		{
			try
			{
				if (item == null)
				{
					throw new ArgumentNullException(nameof(item), "Sản phẩm không được null");
				}

				await _unitOfWork.Item.CreatedAsync(item);
				return item;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm sản phẩm: {ex.Message}");
				throw;
			}
		}

		public async Task<Item> UpdateItemAsync(Item item)
		{
			try
			{
				if (item == null)
				{
					throw new ArgumentNullException(nameof(item), "Sản phẩm không được null");
				}

				var existingProduct = await _unitOfWork.Item.GetByIdAsync(item.ItemId);
				if (existingProduct == null)
				{
					throw new ArgumentException($"Không tìm thấy sản phẩm với ID: {item.ItemId}");
				}

				await _unitOfWork.Item.UpdateAsync(item);
				return item;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật sản phẩm: {ex.Message}");
				throw;
			}
		}
	}
}
