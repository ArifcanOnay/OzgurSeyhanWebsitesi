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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Primary Key
            builder.HasKey(u => u.Id);

            // KullaniciAdi
            builder.Property(u => u.KullaniciAdi)
                   .IsRequired()
                   .HasMaxLength(50); // Maksimum 50 karakter

            // Email
            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

      

            // PasswordHash ve Salt
            builder.Property(u => u.PasswordHash)
                   .IsRequired();

            builder.Property(u => u.PassworhSalt)
                   .IsRequired();

            // Role
            builder.Property(u => u.Role)
                   .IsRequired();
        }
    }
}
