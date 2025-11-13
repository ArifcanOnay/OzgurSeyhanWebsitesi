using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Repository;
using OzgurSeyhanWebSitesi.Core.Repositories;
using OzgurSeyhanWebSitesi.Repository.Repositories;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using OzgurSeyhanWebSitesi.Repository.UnitOfWorks;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Bussinies.Services;
using System.Reflection;

namespace OzgurSeyhanWebSitesi.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Controllers
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // DbContext
            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))?.GetName().Name);
                });
            });

            // AutoMapper
            builder.Services.AddAutoMapper(typeof(OzgurSeyhanWebSitesi.Bussinies.Mapping.MapProfile));

            // Repository Pattern - Generic
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWorks, UnitofWorks>();

            // Repository Pattern - Specific
            builder.Services.AddScoped<IOgretmenRepository, OgretmenRepository>();
            builder.Services.AddScoped<IYoutubeVideoRepository, YoutubeVideoRepostiroy>();
            builder.Services.AddScoped<IOzelDersRepository, OzelDersRepository>();
            builder.Services.AddScoped<IPodcastRepository, PodcastRepository>();
            builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();

            // Services - Generic
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Services - Specific
            builder.Services.AddScoped<IOgretmenService, OgretmenService>();
            builder.Services.AddScoped<IYoutubeVideoService, YoutubeVideoService>();
            builder.Services.AddScoped<IOzelDersService, OzelDersService>();
            builder.Services.AddScoped<IPodcsatService, PodcastService>();
            builder.Services.AddScoped<IPlaylistService, PlaylistService>();

            // YouTube API Service
            builder.Services.AddScoped<IYoutubeApiService, YoutubeApiService>();

            // Cache Service - Singleton (Memory Cache)
            builder.Services.AddSingleton<PlaylistCacheService>();

            var constr = builder.Configuration.GetConnectionString("DefaultConnection");

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