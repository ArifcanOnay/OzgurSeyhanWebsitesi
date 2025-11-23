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
            
            // CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("https://localhost:7276", "http://localhost:5276") // UI portu
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials(); // Session için gerekli
                });
            });

            // Controllers with JSON options
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // camelCase ve PascalCase'i kabul et
                });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Özgür Seyhan Web Sitesi API",
                    Version = "v1"
                });
                // XML yorumlarını etkinleştir
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
            
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
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IVideoProgressRepository, VideoProgressRepository>();

            // Services - Generic
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Services - Specific
            builder.Services.AddScoped<IOgretmenService, OgretmenService>();
            builder.Services.AddScoped<IYoutubeVideoService, YoutubeVideoService>();
            builder.Services.AddScoped<IOzelDersService, OzelDersService>();
            builder.Services.AddScoped<IPodcsatService, PodcastService>();
            builder.Services.AddScoped<IPlaylistService, PlaylistService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IVideoProgressService, VideoProgressService>();

            // YouTube API Service
            builder.Services.AddScoped<IYoutubeApiService, YoutubeApiService>();

            // Cache Service - Singleton (Memory Cache)
            builder.Services.AddSingleton<PlaylistCacheService>();

            // Session Support
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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
            app.UseCors("AllowAll"); // CORS ekle
            app.UseSession(); // Session middleware ekle
            app.UseAuthorization();
            app.UseRouting();

            // API controllers
            app.MapControllers();

            app.Run();
        }  
    }
}