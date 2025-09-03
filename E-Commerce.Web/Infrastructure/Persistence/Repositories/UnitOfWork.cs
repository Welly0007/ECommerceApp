using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;

namespace Persistence.Repositories
{
	public class UnitOfWork(AppDbContext _context) : IUnitOfWork
	{

		private readonly Dictionary<Type, object> _repositories = [];

		public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
		{
			//1. check if repo exist
			var type = typeof(TEntity);
			if (_repositories.ContainsKey(type))
			{
				// if yes rerurn it
				return (IGenericRepository<TEntity>)_repositories[type];
			}
			//2. if not 
			//create it
			var repository = new GenericRepository<TEntity>(_context);
			// store it in dictionary
			_repositories[type] = repository;
			// return it
			return repository;

		}
		public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
	}
}
