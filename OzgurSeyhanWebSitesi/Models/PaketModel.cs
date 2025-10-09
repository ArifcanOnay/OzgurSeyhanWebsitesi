namespace OzgurSeyhanWebSitesi.Models
{
    public class Paket
    {
        public Guid Id { get; set; }
        public string PaketAdi { get; set; } = string.Empty;
        public int SinifMevcudu { get; set; } // 3 kişilik, 7 kişilik vs.
        public decimal Fiyat { get; set; }
        public string WhatsAppGrupLinki { get; set; } = string.Empty;
        public bool Aktif { get; set; } = true;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign Keys
        public Guid KursId { get; set; }
        public Kurs Kurs { get; set; } = null!;
        
        // Navigation Properties
        public ICollection<SatinAlma> SatinAlmalar { get; set; } = new List<SatinAlma>();
    }
}
