using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OzgurSeyhanWebSitesi.Models
{
    public class Kurs
    {
        public int Id { get; set; }
        public string KursAdi { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public string Konular { get; set; } = string.Empty; // İşlenecek konular
        public string BeklenenSeviye { get; set; } = string.Empty; // Kurs bitiminde ulaşılacak seviye
        public int KursSeviyesi { get; set; } // 1, 2, 3, 4...
        public bool Aktif { get; set; } = true;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
        
        // Kurs Süre ve Program Bilgileri
        public int KursSuresiHafta { get; set; } // Kaç hafta sürecek
        public int HaftalikDersSayisi { get; set; } = 3; // Haftada kaç ders (default 3)
        public int ToplamDersSayisi { get; set; } // Toplam ders sayısı
        public string DersGunleri { get; set; } = string.Empty; // "Pazartesi, Çarşamba, Cuma"
        public string DersSaatleri { get; set; } = string.Empty; // "19:00-20:00"
        
        // Fiyat Bilgileri
        public decimal EskiFiyat { get; set; } // İndirimli fiyat göstermek için
        public decimal YeniFiyat { get; set; } // Güncel fiyat
        public bool IndirimdeMi { get; set; } = false; // İndirim var mı?
        
        // Kurs Renk Teması (UI için)
        public string RenkKodu { get; set; } = "#007bff"; // Default mavi
        public bool PopulerMi { get; set; } = false; // Popüler rozeti için

        // Foreign Keys
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!;

        // Navigation Properties
        public ICollection<Paket> Paketler { get; set; } = new List<Paket>();
        public ICollection<OnceSonra> OnceSonralar { get; set; } = new List<OnceSonra>();
        public ICollection<KursIcerik> KursIcerikleri { get; set; } = new List<KursIcerik>();

    }
}
