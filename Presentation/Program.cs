using AutoMapper;
using BusinessLogic;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using DataAccess.SecurityModels;
using Entities;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Serilog;
using UI.Services;

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

                builder.Services.ConfigureApplicationCookie(cfg =>
                {
                    cfg.Events.OnRedirectToLogin = context =>
                    {
                        var returnUrl = context.Request.Path + context.Request.QueryString;

                        if (context.Request.Path.StartsWithSegments("/Admin"))
                        {
                            context.Response.Redirect(QueryHelpers.AddQueryString("/Admin/Account/Login", "returnUrl", returnUrl));
                        }

                        else
                        {
                            context.Response.Redirect(QueryHelpers.AddQueryString("/Account/Login", "returnUrl", returnUrl));
                        }

                        return Task.CompletedTask;
                    };

                    cfg.Events.OnRedirectToAccessDenied = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/Admin"))
                        {
                            context.Response.Redirect("/Admin/Users/AccessDenied");
                        }

                        else
                        {
                            context.Response.Redirect("/Users/AccessDenied");
                        }

                        return Task.CompletedTask;
                    };
                });

                builder.Services.AddApplicationServices(connectionString);

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
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}")
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
