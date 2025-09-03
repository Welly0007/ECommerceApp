using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
	public static class SpecificationsEvaluator
	{
		public static IQueryable<TEntity> GetQuery<TEntity>(
		IQueryable<TEntity> inputQuery,
		AbsSpecification<TEntity> spec)
			where TEntity : BaseEntity
		{
			var query = inputQuery;
			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}

			if (spec.Includes != null)
			{
				foreach (var include in spec.Includes)
				{
					query = query.Include(include);
				}
			}

			if (spec.OrderBy != null)
			{
				query = query.OrderBy(spec.OrderBy);
			}
			else if (spec.OrderByDescending != null)
			{
				query = query.OrderByDescending(spec.OrderByDescending);
			}
			if (spec.Skip > 0)
			{
				query = query.Skip(spec.Skip);
			}
			if (spec.Take > 0)
			{
				query = query.Take(spec.Take);
			}
			return query;
		}
		public static IQueryable<TEntity> GetQueryForCount<TEntity>(
IQueryable<TEntity> inputQuery,
AbsSpecification<TEntity> spec)
	where TEntity : BaseEntity
		{
			var query = inputQuery;

			// Only apply filtering criteria for count operations
			// Skip includes, sorting, and pagination
			if (spec.Criteria != null)
			{
				query = query.Where(spec.Criteria);
			}

			return query;
		}
	}
}
