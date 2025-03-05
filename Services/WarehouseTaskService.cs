using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class WarehouseTaskService
	{
		private readonly IUnitOfWork _unitOfWork;

		public WarehouseTaskService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<WarehouseTask>> GetAllWarehouseTasksAsync()
		{
			var tasks = await _unitOfWork.WarehouseTask.GetAllAsync();
			return tasks ?? new List<WarehouseTask>();
		}

		public async Task<WarehouseTask> GetWarehouseTaskByIdAsync(int id)
		{
			try
			{
				var task = await _unitOfWork.WarehouseTask.GetByIdAsync(id);
				return task;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy nhiệm vụ kho: {ex.Message}");
				throw;
			}
		}

		public async Task<WarehouseTask> AddWarehouseTaskAsync(WarehouseTask task)
		{
			try
			{
				if (task == null)
				{
					throw new ArgumentNullException(nameof(task), "Nhiệm vụ kho không được null");
				}
				await _unitOfWork.WarehouseTask.CreatedAsync(task);
				await _unitOfWork.SaveChangesAsync();
				return task;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm nhiệm vụ kho: {ex.Message}");
				throw;
			}
		}

		public async Task<WarehouseTask> UpdateWarehouseTaskAsync(WarehouseTask task)
		{
			try
			{
				if (task == null)
				{
					throw new ArgumentNullException(nameof(task), "Nhiệm vụ kho không được null");
				}
				var existingTask = await _unitOfWork.WarehouseTask.GetByIdAsync(task.TaskId);
				if (existingTask == null)
				{
					throw new ArgumentException($"Không tìm thấy nhiệm vụ kho với ID: {task.TaskId}");
				}
				await _unitOfWork.WarehouseTask.UpdateAsync(task);
				await _unitOfWork.SaveChangesAsync();
				return task;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật nhiệm vụ kho: {ex.Message}");
				throw;
			}
		}
	}
}
