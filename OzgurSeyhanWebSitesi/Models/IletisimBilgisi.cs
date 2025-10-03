namespace OzgurSeyhanWebSitesi.Models
{
    public class IletisimBilgisi
    {
        public int Id { get; set; }
        public string TelefonNumarasi { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string YouTubeKanali { get; set; } = string.Empty;
        public string WhatsAppNumarasi { get; set; } = string.Empty;
        public string Adres { get; set; } = string.Empty;
        public string WebSitesi { get; set; } = string.Empty;
        public bool Aktif { get; set; } = true;
        public DateTime GuncellemeTarihi { get; set; } = DateTime.Now;

        // Foreign Keys
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!;
    }
}