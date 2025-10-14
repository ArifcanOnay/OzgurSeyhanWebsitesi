namespace OzgurSeyhanWebSitesi.Models
{
    public class Video
    {
        public Guid Id { get; set; }
        public string VideoBaslik { get; set; } = string.Empty;
        public string YouTubeVideoId { get; set; } = string.Empty; // Örn: "dQw4w9WgXcQ"
        public string Aciklama { get; set; } = string.Empty;
        public string ThumbnailUrl { get; set; } = string.Empty;
        public int Sure { get; set; } // Saniye cinsinden video süresi
        public int SiraNo { get; set; } // Playlist içindeki sıra
        public bool Aktif { get; set; } = true;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign Key
        public Guid PlaylistId { get; set; }
        public Playlist Playlist { get; set; } = null!;
    }
}
