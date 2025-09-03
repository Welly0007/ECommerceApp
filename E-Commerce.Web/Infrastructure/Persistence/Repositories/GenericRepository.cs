using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
	public class GenericRepository<TEntity>(AppDbContext _context) : IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		public async Task AddAsync(TEntity entity) => await _context.SaveChangesAsync();

		public void Delete(TEntity entity) => _context.Remove(entity);

		public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

		public async Task<IEnumerable<TEntity>> GetAllAsync(AbsSpecification<TEntity> specification)
		{
			return await SpecificationsEvaluator.GetQuery(_context.Set<TEntity>(), specification).ToListAsync();
		}

		public async Task<TEntity?> GetByIdAsync(int id) => await _context.Set<TEntity>().FindAsync(id);

		public void Update(TEntity entity) => _context.Update(entity);
		public async Task<int> CountAsync(AbsSpecification<TEntity> spec) => await SpecificationsEvaluator.GetQueryForCount(_context.Set<TEntity>(), spec).CountAsync();
	}
}
