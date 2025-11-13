namespace OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos
{
    public class CreateYoutubeVideoDto
    {
        public string Baslik { get; set; }
        public string Url { get; set; }
        public string VideoId { get; set; }
        public int OgretmenId { get; set; }
    }
}
