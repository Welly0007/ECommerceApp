using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;

namespace E_Commerce.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WeatherForecastController(AppDbContext _context) : ControllerBase
	{
		[HttpGet(Name = "GetWeatherForecast")]

		public IEnumerable<Product> Get()
		{
			var products = _context.Products.ToList();
			return (products);

		}
	}
}
