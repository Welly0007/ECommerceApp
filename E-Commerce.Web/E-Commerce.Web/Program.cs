using System.Text.Json.Serialization;
using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using Service.Mappers;
using ServiceAbstraction;

namespace E_Commerce.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Add services to the container
			builder.Services.AddControllers()
				.AddJsonOptions(options =>
				{
					options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddScoped<IDataSeeding, Dataseeding>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<PictureUrlResolver>();
			// Working for version <=15
			//builder.Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AddMaps(typeof(Service.AssemblyReference).Assembly);
			});
			builder.Services.AddScoped<IServiceManager, ServiceManager>();

			#endregion

			var app = builder.Build();


			#region SeedingData
			var scope = app.Services.CreateScope();
			var objectDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
			objectDataSeeding.SeedDataAsync();
			#endregion

			#region Configure the HTTP request pipeline
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.MapControllers();
			#endregion

			app.Run();

		}
	}
}
