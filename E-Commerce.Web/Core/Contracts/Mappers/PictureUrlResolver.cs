using AutoMapper;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObjects;

namespace Service.Mappers
{
	public class PictureUrlResolver(IConfiguration _config) : IValueResolver<Product, ProductDto, string>
	{
		public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
		{
			if (string.IsNullOrEmpty(source.PictureUrl))
				return string.Empty;
			else
			{
				var url = $"{_config.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
				return url;
			}
		}
	}
}
