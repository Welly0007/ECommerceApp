using DomainLayer.Models;

namespace DomainLayer.Contracts
{
	public interface IGenericRepository<TEntity> where TEntity : BaseEntity
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<IEnumerable<TEntity>> GetAllAsync(AbsSpecification<TEntity> specification);

		Task<TEntity?> GetByIdAsync(int id);
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task<int> CountAsync(AbsSpecification<TEntity> spec);
	}
}
