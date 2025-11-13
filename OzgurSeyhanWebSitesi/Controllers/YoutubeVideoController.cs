using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.Dtos.YoutubeVideoDtos;

namespace OzgurSeyhanWebSitesi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YoutubeVideoController : ControllerBase
    {
        private readonly IYoutubeVideoService _youtubeVideoService;

        public YoutubeVideoController(IYoutubeVideoService youtubeVideoService)
        {
            _youtubeVideoService = youtubeVideoService;
        }

        /// <summary>
        /// Tüm YouTube videolarını getirir
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var videos = _youtubeVideoService.GetAll();
            return Ok(videos);
        }

        /// <summary>
        /// ID'ye göre YouTube videosu getirir
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var video = _youtubeVideoService.GetByIdAsync(id);
            if (video == null)
                return NotFound($"ID {id} ile video bulunamadı");
            
            return Ok(video);
        }

        /// <summary>
        /// YouTube URL'inden video bilgilerini çekip veritabanına kaydeder
        /// </summary>
        [HttpPost("from-url")]
        public async Task<IActionResult> CreateFromYouTubeUrl([FromBody] CreateFromYouTubeUrlRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.YoutubeUrl))
                    return BadRequest("YouTube URL'i boş olamaz");

                if (request.OgretmenId <= 0)
                    return BadRequest("Geçerli bir Öğretmen ID'si giriniz");

                var result = await _youtubeVideoService.CreateFromYouTubeUrlAsync(request.YoutubeUrl, request.OgretmenId);
                
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// YouTube Playlist URL'inden tüm videoları çekip veritabanına kaydeder
        /// </summary>
        [HttpPost("from-playlist")]
        public async Task<IActionResult> CreateFromPlaylist([FromBody] CreateFromPlaylistRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.PlaylistUrl))
                    return BadRequest("YouTube Playlist URL'i boş olamaz");

                if (request.OgretmenId <= 0)
                    return BadRequest("Geçerli bir Öğretmen ID'si giriniz");

                var results = await _youtubeVideoService.CreateFromPlaylistAsync(request.PlaylistUrl, request.OgretmenId);
                
                return Ok(new 
                { 
                    message = $"{results.Count} video başarıyla eklendi",
                    videos = results 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// YouTube videosunu günceller
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateYoutubeVideoDto updateDto)
        {
            try
            {
                var existingVideo = _youtubeVideoService.GetByIdAsync(id);
                if (existingVideo == null)
                    return NotFound($"ID {id} ile video bulunamadı");

                existingVideo.Baslik = updateDto.Baslik;
                existingVideo.Url = updateDto.Url;
                existingVideo.VideoId = updateDto.VideoId;
                existingVideo.OgretmenId = updateDto.OgretmenId;
                existingVideo.UpdateDate = DateTime.Now;

                _youtubeVideoService.Update(existingVideo);
                return Ok("Video başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// YouTube videosunu siler
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var video = _youtubeVideoService.GetByIdAsync(id);
                if (video == null)
                    return NotFound($"ID {id} ile video bulunamadı");

                _youtubeVideoService.Delete(id);
                return Ok("Video başarıyla silindi");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    /// <summary>
    /// YouTube URL'inden video oluşturma request modeli
    /// </summary>
    public class CreateFromYouTubeUrlRequest
    {
        public string YoutubeUrl { get; set; } = string.Empty;
        public int OgretmenId { get; set; }
    }

    /// <summary>
    /// YouTube Playlist'ten videolar oluşturma request modeli
    /// </summary>
    public class CreateFromPlaylistRequest
    {
        public string PlaylistUrl { get; set; } = string.Empty;
        public int OgretmenId { get; set; }
    }
}
