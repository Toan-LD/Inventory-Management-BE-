using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		private readonly InventoryManagementDbContext _context;
		private readonly DbSet<T> _dbSet;

		public GenericRepository(InventoryManagementDbContext context)
		{
			_context = context;
			_dbSet = context.Set<T>();
		}

		public async Task CreatedAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetByAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.FirstOrDefaultAsync(predicate);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}
