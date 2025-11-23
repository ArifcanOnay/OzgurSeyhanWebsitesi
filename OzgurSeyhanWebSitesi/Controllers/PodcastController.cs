using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.PodcastDtos;
using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Services;

namespace OzgurSeyhanWebSitesi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastController : ControllerBase
    {
        private readonly IPodcsatService _podcastService;
        private readonly IMapper _mapper;

        public PodcastController(IPodcsatService podcastService, IMapper mapper)
        {
            _podcastService = podcastService;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm podcast'leri getir
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var podcasts = _podcastService.GetAll();
                var podcastDtos = _mapper.Map<List<PodcastDto>>(podcasts);
                return Ok(podcastDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Podcast'ler getirilirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// ID'ye göre podcast getir
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var podcast = _podcastService.GetByIdAsync(id);
                if (podcast == null)
                    return NotFound(new { message = $"ID {id} ile podcast bulunamadı" });

                var podcastDto = _mapper.Map<PodcastDto>(podcast);
                return Ok(podcastDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Podcast getirilirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Yeni podcast oluştur (Manuel - Spotify embed link ile)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePodcastDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // URL doğrulama (opsiyonel)
                if (string.IsNullOrWhiteSpace(createDto.PodcastUrl))
                    return BadRequest(new { message = "Podcast URL boş olamaz" });

                var podcast = _mapper.Map<Podcast>(createDto);

                await _podcastService.AddAsync(podcast);

                var podcastDto = _mapper.Map<PodcastDto>(podcast);
                return CreatedAtAction(nameof(GetById), new { id = podcast.Id }, podcastDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Podcast oluşturulurken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Podcast güncelle
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePodcastDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != updateDto.Id)
                    return BadRequest(new { message = "URL'deki ID ile body'deki ID eşleşmiyor" });

                var existingPodcast = _podcastService.GetByIdAsync(id);
                if (existingPodcast == null)
                    return NotFound(new { message = $"ID {id} ile podcast bulunamadı" });

                // Map ile güncelle
                _mapper.Map(updateDto, existingPodcast);

                _podcastService.Update(existingPodcast);

                var podcastDto = _mapper.Map<PodcastDto>(existingPodcast);
                return Ok(podcastDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Podcast güncellenirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Podcast sil
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var podcast = _podcastService.GetByIdAsync(id);
                if (podcast == null)
                    return NotFound(new { message = $"ID {id} ile podcast bulunamadı" });

                _podcastService.Delete(id);
                return NoContent(); // 204 - Başarıyla silindi
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Podcast silinirken hata: {ex.Message}" });
            }
        }
    }
}
