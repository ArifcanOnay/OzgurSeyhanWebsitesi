using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace OzgurSeyhanWebSitesi.Models
{
    public class Playlist
    {
        public Guid Id { get; set; }
        public string PlaylistAdi { get; set; } = string.Empty; // "Sıfatlar", "Zarflar", "Fiiller"
        public string Aciklama { get; set; } = string.Empty;
        public string YouTubePlaylistId { get; set; } = string.Empty; // YouTube'dan alınan playlist ID
        public string ThumbnailUrl { get; set; } = string.Empty; // Playlist kapak görseli
        public int SiraNo { get; set; } // Gösterim sırası
        public bool Aktif { get; set; } = true;
        public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;

        // Foreign Key
        public Guid OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!;

        // Navigation Property
        public ICollection<Video> Videolar { get; set; } = new List<Video>();
    }


}
