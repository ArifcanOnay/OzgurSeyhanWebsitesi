using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.VideoProgressDtos;
using OzgurSeyhanWebSitesi.Core.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoProgressController : ControllerBase
    {
        private readonly IVideoProgressService _progressService;

        public VideoProgressController(IVideoProgressService progressService)
        {
            _progressService = progressService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProgress([FromBody] UpdateVideoProgressDto updateDto)
        {
            try
            {
                // Session'dan userId al
                var userId = HttpContext.Session.GetInt32("UserId");
                
                if (userId == null)
                {
                    // Kullanıcı giriş yapmamışsa, LocalStorage'dan devam edilecek (frontend'de)
                    return Ok(new { success = true, message = "Progress saved locally", useLocalStorage = true });
                }

                updateDto.UserId = userId.Value;
                var progress = await _progressService.UpdateProgressAsync(updateDto);
                
                return Ok(new { success = true, data = progress, message = "Progress kaydedildi!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("user/{userId}/video/{videoId}")]
        public async Task<IActionResult> GetProgress(int userId, string videoId)
        {
            try
            {
                var progress = await _progressService.GetProgressAsync(userId, videoId);
                return Ok(new { success = true, data = progress });
            }
            catch (Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("user/{userId}/playlist/{playlistId}")]
        public async Task<IActionResult> GetPlaylistProgress(int userId, int playlistId)
        {
            try
            {
                var progresses = await _progressService.GetUserPlaylistProgressAsync(userId, playlistId);
                var summary = await _progressService.GetPlaylistProgressSummaryAsync(userId, playlistId);
                
                return Ok(new { success = true, data = progresses, summary = summary });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("user/{userId}/all")]
        public async Task<IActionResult> GetAllUserProgress(int userId)
        {
            try
            {
                var progresses = await _progressService.GetAllUserProgressAsync(userId);
                return Ok(new { success = true, data = progresses });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("current-user/playlist/{playlistId}")]
        public async Task<IActionResult> GetCurrentUserPlaylistProgress(int playlistId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            
            if (userId == null)
            {
                return Unauthorized(new { success = false, message = "Lütfen giriş yapın!" });
            }

            try
            {
                var progresses = await _progressService.GetUserPlaylistProgressAsync(userId.Value, playlistId);
                var summary = await _progressService.GetPlaylistProgressSummaryAsync(userId.Value, playlistId);
                
                return Ok(new { success = true, data = progresses, summary = summary });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
