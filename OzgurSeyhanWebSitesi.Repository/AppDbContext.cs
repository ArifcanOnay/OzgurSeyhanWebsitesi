using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository
{
    public class AppDbContext(DbContextOptions<AppDbContext> options):DbContext(options)
    {
        public DbSet<Ogretmen> Ogretmenler {  get; set; }
        public DbSet<YoutubeVideo> YoutubeVideoları {  get; set; }
        public DbSet<Podcast> Podcastler {  get; set; }
        public DbSet<OzelDers>OzelDersler { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
