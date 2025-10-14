using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Models;

namespace OzgurSeyhanWebSitesi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }
        public DbSet<Kurs> Kurslar { get; set; }
        public DbSet<OrnekVideo> OrnekVideolar { get; set; }
        public DbSet<Paket> Paketler { get; set; }
        public DbSet<OnceSonra> OnceSonralar { get; set; }
        public DbSet<SatinAlma> SatinAlmalar { get; set; }
        public DbSet<KursIcerik> KursIcerikleri { get; set; }
        public DbSet<IletisimBilgisi> IletisimBilgileri { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Video> Videos { get; set; }

        // YouTube Related DbSets
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ogretmen → Kurs: One-to-Many
            modelBuilder.Entity<Kurs>()
                .HasOne(k => k.Ogretmen)
                .WithMany(o => o.Kurslar)
                .HasForeignKey(k => k.OgretmenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ogretmen → OrnekVideo: One-to-Many
            modelBuilder.Entity<OrnekVideo>()
                .HasOne(ov => ov.Ogretmen)
                .WithMany(o => o.OrnekVideolar)
                .HasForeignKey(ov => ov.OgretmenId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kurs → Paket: One-to-Many
            modelBuilder.Entity<Paket>()
                .HasOne(p => p.Kurs)
                .WithMany(k => k.Paketler)
                .HasForeignKey(p => p.KursId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kurs → OnceSonra: One-to-Many
            modelBuilder.Entity<OnceSonra>()
                .HasOne(os => os.Kurs)
                .WithMany(k => k.OnceSonralar)
                .HasForeignKey(os => os.KursId)
                .OnDelete(DeleteBehavior.Cascade);

            // Paket → SatinAlma: One-to-Many
            modelBuilder.Entity<SatinAlma>()
                .HasOne(sa => sa.Paket)
                .WithMany(p => p.SatinAlmalar)
                .HasForeignKey(sa => sa.PaketId)
                .OnDelete(DeleteBehavior.Cascade);

            // Kurs → KursIcerik: One-to-Many
            modelBuilder.Entity<KursIcerik>()
                .HasOne(ki => ki.Kurs)
                .WithMany(k => k.KursIcerikleri)
                .HasForeignKey(ki => ki.KursId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ogretmen → IletisimBilgisi: One-to-Many
            modelBuilder.Entity<IletisimBilgisi>()
                .HasOne(ib => ib.Ogretmen)
                .WithMany(o => o.IletisimBilgileri)
                .HasForeignKey(ib => ib.OgretmenId)
                .OnDelete(DeleteBehavior.Cascade);

           

            // Entity Configurations
            ConfigureOgretmen(modelBuilder);
            ConfigureKurs(modelBuilder);
            ConfigureOrnekVideo(modelBuilder);
            ConfigurePaket(modelBuilder);
            ConfigureOnceSonra(modelBuilder);
            ConfigureSatinAlma(modelBuilder);
            ConfigureKursIcerik(modelBuilder);
            ConfigureIletisimBilgisi(modelBuilder);
            
        }

        private void ConfigureOgretmen(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ogretmen>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AdSoyad)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Ozgecmis)
                    .HasMaxLength(2000);
                entity.Property(e => e.Egitim)
                    .HasMaxLength(500);
                entity.Property(e => e.ProfilFotoUrl)
                    .HasMaxLength(500);
                entity.Property(e => e.YouTubeKanalUrl)
                    .HasMaxLength(500);
                entity.Property(e => e.OlusturmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureKurs(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Kurs>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.KursAdi)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(2000);
                entity.Property(e => e.Konular)
                    .HasMaxLength(2000);
                entity.Property(e => e.BeklenenSeviye)
                    .HasMaxLength(500);
                entity.Property(e => e.DersGunleri)
                    .HasMaxLength(100);
                entity.Property(e => e.DersSaatleri)
                    .HasMaxLength(50);
                entity.Property(e => e.EskiFiyat)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.YeniFiyat)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.RenkKodu)
                    .HasMaxLength(20);
                entity.Property(e => e.OlusturmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureOrnekVideo(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrnekVideo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Baslik)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.VideoUrl)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(1000);
                entity.Property(e => e.DersYontemi)
                    .HasMaxLength(500);
                entity.Property(e => e.OlusturmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigurePaket(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PaketAdi)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Fiyat)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.WhatsAppGrupLinki)
                    .HasMaxLength(500);
                entity.Property(e => e.OlusturmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureOnceSonra(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OnceSonra>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.OgrenciAdi)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.OnceVideoUrl)
                    .HasMaxLength(500);
                entity.Property(e => e.SonraVideoUrl)
                    .HasMaxLength(500);
                entity.Property(e => e.OnceAciklama)
                    .HasMaxLength(1000);
                entity.Property(e => e.SonraAciklama)
                    .HasMaxLength(1000);
                entity.Property(e => e.AlinanKurslar)
                    .HasMaxLength(200);
                entity.Property(e => e.OlusturmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureSatinAlma(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SatinAlma>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.MusteriAdi)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.MusteriEmail)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.MusteriTelefon)
                    .HasMaxLength(20);
                entity.Property(e => e.OdenenTutar)
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.OdemeIslemId)
                    .HasMaxLength(100);
                entity.Property(e => e.SatinAlmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureKursIcerik(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KursIcerik>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.IcerikBasligi)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Aciklama)
                    .HasMaxLength(1000);
                entity.Property(e => e.OlusturmaTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

        private void ConfigureIletisimBilgisi(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IletisimBilgisi>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TelefonNumarasi)
                    .HasMaxLength(20);
                entity.Property(e => e.Email)
                    .HasMaxLength(100);
                entity.Property(e => e.YouTubeKanali)
                    .HasMaxLength(200);
                entity.Property(e => e.WhatsAppNumarasi)
                    .HasMaxLength(20);
                entity.Property(e => e.Adres)
                    .HasMaxLength(500);
                entity.Property(e => e.WebSitesi)
                    .HasMaxLength(200);
                entity.Property(e => e.GuncellemeTarihi)
                    .HasDefaultValueSql("GETDATE()");
            });
        }

       
       
        }
    }

