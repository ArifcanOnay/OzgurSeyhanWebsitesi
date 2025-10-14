namespace OzgurSeyhanWebSitesi.Models
{
    public class Ogretmen
    {
        public Guid Id { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public string Ozgecmis { get; set; } = string.Empty;
        public string Egitim { get; set; } = string.Empty;
        public int DeneyimYili { get; set; }
        public string ProfilFotoUrl { get; set; } = string.Empty;
        public string YouTubeKanalUrl { get; set; } = string.Empty;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Navigation Properties
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();
        public ICollection<OrnekVideo> OrnekVideolar { get; set; } = new List<OrnekVideo>();
        public ICollection<IletisimBilgisi> IletisimBilgileri { get; set; } = new List<IletisimBilgisi>();
        public ICollection<Playlist> Playlistler { get; set; } = new List<Playlist>();
    }
}
