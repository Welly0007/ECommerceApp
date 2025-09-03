using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using Shared.ProductSpecifications;

namespace Service
{
	public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
	{
		public async Task<IEnumerable<BrandDto>> GetAllBrands()
		{
			var brands = await _unitOfWork.GetRepository<ProductBrand>().GetAllAsync();
			var brandDtos = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
			return brandDtos;

		}

		public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
		{
			var repo = _unitOfWork.GetRepository<Product>();
			// I need here to set the specifaction for product, it will take queryParams as input
			var spec = new ProductSpecifications(queryParams);
			var totalFoundItems = await repo.CountAsync(spec);

			// then pass the spec to the repo method
			var products = await repo.GetAllAsync(spec);

			//var products = await repo.GetAllAsync();
			var productDtos = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
			return new PaginatedResult<ProductDto>(queryParams.pageIndex, productDtos.Count(), totalFoundItems, productDtos);
		}

		public async Task<IEnumerable<TypeDto>> GetAllTypes()
		{
			var types = await _unitOfWork.GetRepository<ProductType>().GetAllAsync();
			var typeDtos = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
			return typeDtos;
		}

		public async Task<ProductDto> GetProductById(int id)
		{
			var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
			if (product is null)
			{
				return null!;
			}
			var productDto = _mapper.Map<Product, ProductDto>(product);
			return productDto;
		}
	}
}
