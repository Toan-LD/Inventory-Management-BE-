using System.Linq.Expressions;

namespace InventoryManagement.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task CreatedAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
	}
}
