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
    public class KursConfiguration : IEntityTypeConfiguration<Kurs>
    {
        public void Configure(EntityTypeBuilder<Kurs> builder)
        {
            // Primary Key
            builder.HasKey(k => k.Id);

            // Kolonlar
            builder.Property(k => k.KursAdi)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(k => k.Aciklama)
                   .HasMaxLength(500);

            builder.Property(k => k.Seviye)
                   .HasMaxLength(20);

            builder.Property(k => k.IslencekKonular)
                   .HasMaxLength(500);

            builder.Property(k => k.DersSaati)
                   .HasMaxLength(50);

            builder.Property(k => k.KursunSonundaEldeEdilecekYetenekler)
                   .HasMaxLength(500);

            builder.Property(k => k.OgrenciSayisi)
                   .IsRequired();

            builder.Property(k => k.HaftadaKacGun)
                   .IsRequired();

            builder.Property(k => k.BaslangicTarihi)
                   .IsRequired();

            builder.Property(k => k.BitisTarihi)
                   .IsRequired();

            builder.Property(k => k.HaftanınHangiGünleri)
                   .HasMaxLength(50);

            // Relationship: Bir kursun bir öğretmeni olabilir
            builder.HasOne(k => k.Ogretmen)
                   .WithMany(o => o.Kurslar)
                   .HasForeignKey(k => k.OgretmenId)
                   .OnDelete(DeleteBehavior.Cascade); // Öğretmen silinirse kurslar da silinsin
        }
    }
    }
