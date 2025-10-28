using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository.Configurations
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            // Primary Key
        builder.HasKey(v => v.Id);

            // Kolonlar
            builder.Property(v => v.Baslik)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(v => v.Açıklama)
                   .HasMaxLength(500);

            builder.Property(v => v.YoutubeUrl)
                   .IsRequired()
                   .HasMaxLength(250);

            // Relationship: bir video bir öğretmene ait
            builder.HasOne(v => v.Ogretmen)
                   .WithMany(o => o.Videolar)
                   .HasForeignKey(v => v.OgretmenId)
                   .OnDelete(DeleteBehavior.Cascade); // Öğretmen silinirse videolar da silinsin
        }
    }
}
