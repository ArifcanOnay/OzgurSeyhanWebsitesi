namespace OzgurSeyhanWebSitesi.Models
{
    public class KursIcerik
    {
        public Guid Id { get; set; }
        public string IcerikBasligi { get; set; } = string.Empty; // "Temel gramer yapıları", "Günlük konuşma kalıpları" vs.
        public string Aciklama { get; set; } = string.Empty; // İçerik detayı
        public int SiraNo { get; set; } // İçeriğin sırası
        public bool Aktif { get; set; } = true;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign Keys
        public Guid KursId { get; set; }
        public Kurs Kurs { get; set; } = null!;
    }
}