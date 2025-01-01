using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project1.Data;
using Project1.Models;
using Project1.Repository.IRepository;
using Project1.Repository.Repository;
using System.Runtime.CompilerServices;

namespace Project1.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddServiceExtensions(this IServiceCollection services , IConfiguration configuration  )
        {
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
            });
            services.AddCors();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBuyerProduct, BuyerProduct>();

            return services;

            
        }
       


    }
}
