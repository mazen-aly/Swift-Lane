using AutoMapper;
using BusinessLogic;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
                });

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("No database connection string was found.");

                // Add services to the container.
                builder.Services.AddControllersWithViews();
                builder.Services.AddDbContext<SwiftLaneDbContext>(options => options.UseSqlServer(connectionString));
                builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                builder.Services.AddScoped<IShippingTypeService, ShippingTypeService>();
                builder.Services.AddAutoMapper(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                });


                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseAuthorization();

                app.MapStaticAssets();


                app.MapControllerRoute(
                    name: "admin",
                    pattern: "{area=Admin}/{controller=Home}/{action=Index}/{id?}")
                    .WithStaticAssets();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                    .WithStaticAssets();


                app.Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
