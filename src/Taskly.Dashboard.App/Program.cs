using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Taskly.Dashboard.App.Data;
using Taskly.Dashboard.App.Repositories;
using Taskly.Dashboard.App.Repositories.Interfaces;
using Taskly.Dashboard.App.Services;
using Taskly.Dashboard.App.Services.Interfaces;
using Taskly.Dashboard.App.Configurations;

namespace Taskly.Dashboard.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));

            builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var databaseOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
                options.UseSqlServer(databaseOptions.ConnectionString);
            });

            // Register Repository & Service
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
    }
}