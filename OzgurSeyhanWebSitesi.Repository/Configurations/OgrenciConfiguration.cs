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
    public class OgrenciConfiguration : IEntityTypeConfiguration<Ogrenci>
    {
        public void Configure(EntityTypeBuilder<Ogrenci> builder)
        {
            // Primary Key
            builder.HasKey(o => o.Id);

            // KullaniciAdi
            builder.Property(o => o.KullaniciAdi)
                   .IsRequired()
                   .HasMaxLength(50);

            // Email
            builder.Property(o => o.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            // PasswordHash
            builder.Property(o => o.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(250); // Hash uzunluğuna göre ayarla

            
        }
    }
}
