using Microsoft.EntityFrameworkCore;
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
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddUserSecrets<Program>();
            }

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register Repository & Service
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

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