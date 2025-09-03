using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObjects;
using Shared.ProductSpecifications;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductController(IServiceManager serviceManager) : ControllerBase
	{
		//Get ALL
		[HttpGet]
		public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams QueryParams)
		{
			var productsPaginated = await serviceManager.ProductService.GetAllProductsAsync(QueryParams);
			return Ok(productsPaginated);
		}
		//GetById
		[HttpGet("{id:int}")]
		public async Task<ActionResult<ProductDto>> GetProductById(int id)
		{
			var product = await serviceManager.ProductService.GetProductById(id);
			return Ok(product);
		}
		[HttpGet("types")]
		public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
		{
			var types = await serviceManager.ProductService.GetAllTypes();
			return Ok(types);
		}
		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
		{
			var brands = await serviceManager.ProductService.GetAllBrands();
			return Ok(brands);
		}


	}
}
