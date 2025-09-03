using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjects;

namespace Service.Mappers
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
				.ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.ProductType.Name))
				.ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());
			CreateMap<ProductBrand, BrandDto>();
			CreateMap<ProductType, TypeDto>();
		}
	}
}
