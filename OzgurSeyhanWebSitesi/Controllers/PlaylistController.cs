using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.PlaylistDtos;
using OzgurSeyhanWebSitesi.Core.Services;

namespace OzgurSeyhanWebSitesi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        /// <summary>
        /// Tüm playlistleri getirir
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var playlists = _playlistService.GetAll();
            return Ok(playlists);
        }

        /// <summary>
        /// ID'ye göre playlist getirir
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var playlist = _playlistService.GetByIdAsync(id);
            if (playlist == null)
                return NotFound($"ID {id} ile playlist bulunamadı");

            return Ok(playlist);
        }

        /// <summary>
        /// Playlist'i videoları ile birlikte getirir (videolar YouTube'dan çekilir)
        /// </summary>
        [HttpGet("{id}/with-videos")]
        public async Task<IActionResult> GetWithVideos(int id)
        {
            try
            {
                var playlist = await _playlistService.GetPlaylistWithVideosAsync(id);
                return Ok(playlist);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Öğretmene ait tüm playlistleri getirir
        /// </summary>
        [HttpGet("by-ogretmen/{ogretmenId}")]
        public async Task<IActionResult> GetByOgretmenId(int ogretmenId)
        {
            try
            {
                var playlists = await _playlistService.GetByOgretmenIdAsync(ogretmenId);
                return Ok(playlists);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// YouTube Playlist URL'inden playlist oluşturur (sadece playlist kaydı, videolar YouTube'dan çekilir)
        /// </summary>
        [HttpPost("from-url")]
        public async Task<IActionResult> CreateFromPlaylistUrl([FromBody] CreatePlaylistDto request)
        {
            try
            {
                

                if (string.IsNullOrWhiteSpace(request.PlaylistUrl))
                    return BadRequest("Playlist URL'i boş olamaz");

                if (request.OgretmenId <= 0)
                    return BadRequest("Geçerli bir Öğretmen ID'si giriniz");

                var result = await _playlistService.CreateFromYouTubePlaylistAsync(
                    playlistUrl: request.PlaylistUrl, 
                    ogretmenId: request.OgretmenId,
                    kategoriBaslik: request.KategoriBaslik);

              

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Playlist'i siler
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var playlist = _playlistService.GetByIdAsync(id);
                if (playlist == null)
                    return NotFound($"ID {id} ile playlist bulunamadı");

                _playlistService.Delete(id);
                return Ok("Playlist başarıyla silindi");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
