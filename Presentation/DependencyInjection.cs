using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DataAccess.SecurityModels;
using Microsoft.EntityFrameworkCore;
using UI.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<SwiftLaneDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
            })
                .AddEntityFrameworkStores<SwiftLaneDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IShippingTypeService, ShippingTypeService>();
            services.AddScoped<IShipmentCarrierService, ShipmentCarrierService>();

            return services;
        }
    }
}
