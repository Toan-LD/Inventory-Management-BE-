using InventoryManagement.Data;
using InventoryManagement.Interfaces;
using InventoryManagement.Repositories;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InventoryManagement.DependencyInjection
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddInventoryService(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<InventoryManagementDbContext>(options =>
			{
				options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
				options.EnableSensitiveDataLogging();
				options.LogTo(Console.WriteLine, LogLevel.Information);
			});


			// Repositories
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IItemRepository, ItemRepository>();
			services.AddScoped<ILocationRepository, LocationRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderItemRepository, OrderItemRepository>();
			services.AddScoped<IStockRepository, StockRepository>();
			services.AddScoped<ISupplierRepository, SupplierRepository>();
			services.AddScoped<ITransactionRepository, TransactionRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IWarehouseTaskRepository, WarehouseTaskRepository>();

			// UnitOfWork
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			// Services
			services.AddScoped<CustomerService>();
			services.AddScoped<ItemService>();
			services.AddScoped<LocationService>();
			services.AddScoped<OrderService>();
			services.AddScoped<OrderItemService>();
			services.AddScoped<StockService>();
			services.AddScoped<SupplierService>();
			services.AddScoped<TransactionService>();
			services.AddScoped<UserService>();
			services.AddScoped<WarehouseTaskService>();


			//JWT
			var jwtSettings = config.GetSection("Jwt");
			var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtSettings["Issuer"],
						ValidAudience = jwtSettings["Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(key)
					};
				});

			//Use Cors
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
			});

			return services;
		}
	}
}
