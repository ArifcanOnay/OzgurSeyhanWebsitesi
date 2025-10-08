namespace OzgurSeyhanWebSitesi.Models
{
    public class SatinAlma
    {
        public Guid Id { get; set; }
        public string MusteriAdi { get; set; } = string.Empty;
        public string MusteriEmail { get; set; } = string.Empty;
        public string MusteriTelefon { get; set; } = string.Empty;
        public decimal OdenenTutar { get; set; }
        public bool OdemeBasarili { get; set; } = false;
        public string OdemeIslemId { get; set; } = string.Empty;
        public DateTime SatinAlmaTarihi { get; set; } = DateTime.Now;

        // Foreign Keys
        public Guid PaketId { get; set; }
        public Paket Paket { get; set; } = null!;
    }
}