namespace OzgurSeyhanWebSitesi.Core.Dtos.PodcastDtos
{
    public class UpdatePodcastDto
    {
        public int Id { get; set; }
        public string Baslik { get; set; } = string.Empty;
        public string PodcastUrl { get; set; } = string.Empty;
        public int OgretmenId { get; set; }
    }
}
