namespace OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos
{
    public class UpdateYoutubeVideoDto
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Url { get; set; }
        public string VideoId { get; set; }
        public int OgretmenId { get; set; }
    }
}
