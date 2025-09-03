using Shared;
using Shared.DataTransferObjects;
using Shared.ProductSpecifications;

namespace ServiceAbstraction
{
	public interface IProductService
	{
		Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
		Task<ProductDto> GetProductById(int id);
		Task<IEnumerable<TypeDto>> GetAllTypes();
		Task<IEnumerable<BrandDto>> GetAllBrands();
	}
}
