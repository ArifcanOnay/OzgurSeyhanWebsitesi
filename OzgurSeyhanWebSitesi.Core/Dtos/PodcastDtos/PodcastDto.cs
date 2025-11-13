using OzgurSeyhanWebSitesi.Core.Dtos;

namespace OzgurSeyhanWebSitesi.Core.Dtos.PodcastDtos
{
    public class PodcastDto : BaseDto
    {
        public string Baslik { get; set; } = string.Empty;
        public string PodcastUrl { get; set; } = string.Empty;
        public int OgretmenId { get; set; }
       
    }
}
