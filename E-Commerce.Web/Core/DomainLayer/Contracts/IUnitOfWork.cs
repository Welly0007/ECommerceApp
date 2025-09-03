using DomainLayer.Models;

namespace DomainLayer.Contracts
{
	public interface IUnitOfWork
	{

		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
		Task<int> CompleteAsync();
	}
}
