

using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Repository;
using System.Reflection;

namespace OzgurSeyhanWebSitesi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });
            
            var constr= builder.Configuration.GetConnectionString("DefaultConnection");

          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
          
            
            app.UseStaticFiles();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRouting();


            // API controllers
            app.MapControllers();

            app.Run();
        }  
    }
}