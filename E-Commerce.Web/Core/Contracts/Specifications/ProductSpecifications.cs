using System.Linq.Expressions;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Shared.ProductSpecifications;

namespace Service.Specifications
{
	public class ProductSpecifications : AbsSpecification<Product>
	{
		public ProductSpecifications(ProductQueryParams queryParams)
		{
			// Add includes for navigation properties
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);

			if (!string.IsNullOrWhiteSpace(queryParams.searchValue))
			{
				var searchQuery = queryParams.searchValue.ToLower();

				switch (queryParams.searchOptions)
				{
					case ProductSearchOptions.ByName:
						Criteria = p => p.Name.ToLower().Contains(searchQuery);
						break;
					case ProductSearchOptions.ByBrand:
						Criteria = p => p.ProductBrand.Name.ToLower().Contains(searchQuery);
						break;
					case ProductSearchOptions.ByType:
						Criteria = p => p.ProductType.Name.ToLower().Contains(searchQuery);
						break;
					case ProductSearchOptions.All:
						Criteria = p => p.Name.ToLower().Contains(searchQuery) ||
						p.ProductBrand.Name.ToLower().Contains(searchQuery) ||
						p.ProductType.Name.ToLower().Contains(searchQuery);
						break;
				}
			}
			if (queryParams.typeId != null)
			{
				Criteria = p => p.TypeId == queryParams.typeId;
			}
			if (queryParams.brandId != null)
			{
				Criteria = p => p.BrandId == queryParams.brandId;
			}

			// Handle sorting
			Expression<Func<Product, object>> sortExpression = queryParams.sortOptions switch
			{
				ProductSortingOptions.Name => p => p.Name,
				ProductSortingOptions.Price => p => p.Price,
				_ => p => p.Id
			};

			AddOrderBy(sortExpression, queryParams.isSortDescending);
			Take = queryParams.pageSize;
			Skip = (queryParams.pageIndex - 1) * Take;
		}
	}
}
