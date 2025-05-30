﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Taskly.Dashboard.App.Configurations;
using Taskly.Dashboard.App.Data;
using Taskly.Dashboard.App.Repositories;
using Taskly.Dashboard.App.Repositories.Interfaces;
using Taskly.Dashboard.App.Services;
using Taskly.Dashboard.App.Services.Interfaces;

namespace Taskly.Dashboard.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load the configuration with different appsettings.json
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            try
            {
                Log.Information("Starting Taskly Web...");

                var builder = WebApplication.CreateBuilder(args);

                // Integrate Serilog into the Host
                builder.Host.UseSerilog();

                if (builder.Environment.IsDevelopment())
                {
                    builder.Configuration.AddUserSecrets<Program>();
                }

                // Configure DatabaseOptions and validate
                builder.Services.AddOptions<DatabaseOptions>()
                    .Bind(builder.Configuration.GetSection(DatabaseOptions.ConfigurationSectionName))
                    .Validate(options =>
                        options is not null && !string.IsNullOrWhiteSpace(options.ConnectionString),
                        $"{nameof(DatabaseOptions.ConnectionString)} is invalid!");

                builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
                {
                    var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                    options.UseSqlServer(databaseOptions.ConnectionString);
                });

                // Register Repositories & Services
                builder.Services.AddUserServices();

                // Register MVC Controllers
                builder.Services.AddControllersWithViews();

                var app = builder.Build();

                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Register}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Taskly terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}