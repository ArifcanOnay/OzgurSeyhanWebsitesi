using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Repository;
using System.Reflection;

namespace OzgurSeyhanWebSitesi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            // Add controllers and views (MVC support)
            builder.Services.AddControllersWithViews();
            var constr= builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(constr, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            }); 

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            // MVC route mapping
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // API controllers
            app.MapControllers();

            app.Run();
        }  
    }
}