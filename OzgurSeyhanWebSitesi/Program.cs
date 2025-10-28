using Microsoft.EntityFrameworkCore;

namespace OzgurSeyhanWebSitesi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            // Add controllers and views (MVC support)
            builder.Services.AddControllersWithViews();

            // Swagger/OpenAPI desteği
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
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