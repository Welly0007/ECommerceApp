using System.Linq.Expressions;
using DomainLayer.Models;

namespace DomainLayer.Contracts
{
	public abstract class AbsSpecification<TEntity> where TEntity : BaseEntity
	{

		public Expression<Func<TEntity, bool>>? Criteria { get; protected set; }
		public List<Expression<Func<TEntity, object>>>? Includes { get; protected set; } = new List<Expression<Func<TEntity, object>>>();

		public Expression<Func<TEntity, object>>? OrderBy { get; protected set; }
		public Expression<Func<TEntity, object>>? OrderByDescending { get; protected set; }
		public int Take { get; protected set; }
		public int Skip { get; protected set; }

		public void AddOrderBy(Expression<Func<TEntity, object>> sortExpression, bool isDescending = false)
		{
			if (isDescending)
			{
				OrderByDescending = sortExpression;
			}
			else
			{
				OrderBy = sortExpression;
			}
		}

		protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
		{
			Includes?.Add(includeExpression);
		}
	}
}
