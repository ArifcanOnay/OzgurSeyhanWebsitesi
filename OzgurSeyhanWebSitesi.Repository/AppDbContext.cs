using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository
{
   public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<Kurs> Kurslar { get; set; }
        public DbSet<Video> Videolar { get; set; }
        public DbSet<Spotify> SpotifyLinkleri { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get;set; }
        public DbSet<User>Users { get; set; }   
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
