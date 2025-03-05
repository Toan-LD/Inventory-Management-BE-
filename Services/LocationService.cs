using InventoryManagement.Interfaces;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public class LocationService
	{
		private readonly IUnitOfWork _unitOfWork;

		public LocationService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IEnumerable<Location>> GetAllLocationsAsync()
		{
			var locations = await _unitOfWork.Location.GetAllAsync();
			return locations ?? new List<Location>();
		}

		public async Task<Location> GetLocationByIdAsync(int id)
		{
			try
			{
				var location = await _unitOfWork.Location.GetByIdAsync(id);
				return location;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi lấy vị trí: {ex.Message}");
				throw;
			}
		}

		public async Task<Location> AddLocationAsync(Location location)
		{
			try
			{
				if (location == null)
				{
					throw new ArgumentNullException(nameof(location), "Vị trí không được null");
				}
				await _unitOfWork.Location.CreatedAsync(location);
				await _unitOfWork.SaveChangesAsync();
				return location;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi thêm vị trí: {ex.Message}");
				throw;
			}
		}

		public async Task<Location> UpdateLocationAsync(Location location)
		{
			try
			{
				if (location == null)
				{
					throw new ArgumentNullException(nameof(location), "Vị trí không được null");
				}
				var existingLocation = await _unitOfWork.Location.GetByIdAsync(location.LocationId);
				if (existingLocation == null)
				{
					throw new ArgumentException($"Không tìm thấy vị trí với ID: {location.LocationId}");
				}
				await _unitOfWork.Location.UpdateAsync(location);
				await _unitOfWork.SaveChangesAsync();
				return location;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi khi cập nhật vị trí: {ex.Message}");
				throw;
			}
		}
	}
}
