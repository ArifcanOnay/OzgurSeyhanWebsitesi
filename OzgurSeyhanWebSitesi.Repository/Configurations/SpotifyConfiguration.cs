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
    public class SpotifyConfiguration : IEntityTypeConfiguration<Spotify>
    {
        public void Configure(EntityTypeBuilder<Spotify> builder)
        {
            // Primary Key
            builder.HasKey(s => s.Id);

            // Kolonlar
            builder.Property(s => s.Başlık)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(s => s.SpotifyUrl)
                   .IsRequired()
                   .HasMaxLength(250);

            builder.Property(s => s.EklenmeTarihi)
                   .HasDefaultValueSql("GETDATE()"); // SQL tarafında default tarih

            // Relationship: bir Spotify linki bir öğretmene ait
            builder.HasOne(s => s.Ogretmen)
                   .WithMany(o => o.SpotifyLinkleri)
                   .HasForeignKey(s => s.OgretmenId)
                   .OnDelete(DeleteBehavior.Cascade); // Öğretmen silinirse linkler de silinsin
        }
    }
}
