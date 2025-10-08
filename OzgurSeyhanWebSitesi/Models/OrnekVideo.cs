namespace OzgurSeyhanWebSitesi.Models
{
    public class OrnekVideo
    {
        public Guid Id { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public string DersYontemi { get; set; } = string.Empty; // Derslerin nasıl işlendiği
        public int SiraNo { get; set; }
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        // Foreign Keys
        public Guid OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!;
    }
}
