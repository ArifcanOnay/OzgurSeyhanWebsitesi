namespace OzgurSeyhanWebSitesi.Models
{
    public class OnceSonra
    {
        public int Id { get; set; }
        public string OgrenciAdi { get; set; } = string.Empty;
        public string OnceVideoUrl { get; set; } = string.Empty;
        public string SonraVideoUrl { get; set; } = string.Empty;
        public string OnceAciklama { get; set; } = string.Empty;
        public string SonraAciklama { get; set; } = string.Empty;
        public string AlinanKurslar { get; set; } = string.Empty; // "1. Kurs + 2. Kurs"
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign Keys
        public int KursId { get; set; }
        public Kurs Kurs { get; set; } = null!;
    }
}
