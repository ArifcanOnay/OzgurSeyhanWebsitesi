using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository.Configurations
{
    public class OgretmenConfiguration : IEntityTypeConfiguration<Ogretmen>
    {
        public void Configure(EntityTypeBuilder<Ogretmen> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Isim)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(o => o.SoyAd)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(o => o.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasMany(o => o.Videolar)
                   .WithOne(v => v.Ogretmen)
                   .HasForeignKey(v => v.OgretmenId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.SpotifyLinkleri)
                   .WithOne(s => s.Ogretmen)
                   .HasForeignKey(s => s.OgretmenId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.Kurslar)
                   .WithOne(k => k.Ogretmen)
                   .HasForeignKey(k => k.OgretmenId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
