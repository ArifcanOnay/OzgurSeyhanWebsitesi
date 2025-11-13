using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.OgretmenDtos;
using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Services;

namespace OzgurSeyhanWebSitesi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OgretmenController : ControllerBase
    {
        private readonly IOgretmenService _ogretmenService;
        private readonly IMapper _mapper;

        public OgretmenController(IOgretmenService ogretmenService, IMapper mapper)
        {
            _ogretmenService = ogretmenService;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm öğretmenleri getir
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var ogretmenler = _ogretmenService.GetAll();
                var ogretmenDtos = _mapper.Map<List<OgretmenDto>>(ogretmenler);
                return Ok(ogretmenDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Öğretmenler getirilirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// ID'ye göre öğretmen getir
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var ogretmen = _ogretmenService.GetByIdAsync(id);
                if (ogretmen == null)
                    return NotFound(new { message = $"ID {id} ile öğretmen bulunamadı" });

                var ogretmenDto = _mapper.Map<OgretmenDto>(ogretmen);
                return Ok(ogretmenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Öğretmen getirilirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Yeni öğretmen oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOgretmenDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ogretmen = _mapper.Map<Ogretmen>(createDto);

                await _ogretmenService.AddAsync(ogretmen);

                var ogretmenDto = _mapper.Map<OgretmenDto>(ogretmen);
                return CreatedAtAction(nameof(GetById), new { id = ogretmen.Id }, ogretmenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Öğretmen oluşturulurken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Öğretmen güncelle
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateOgretmenDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != updateDto.Id)
                    return BadRequest(new { message = "URL'deki ID ile body'deki ID eşleşmiyor" });

                var existingOgretmen = _ogretmenService.GetByIdAsync(id);
                if (existingOgretmen == null)
                    return NotFound(new { message = $"ID {id} ile öğretmen bulunamadı" });

                // Map ile güncelle
                _mapper.Map(updateDto, existingOgretmen);

                _ogretmenService.Update(existingOgretmen);

                var ogretmenDto = _mapper.Map<OgretmenDto>(existingOgretmen);
                return Ok(ogretmenDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Öğretmen güncellenirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Öğretmen sil
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ogretmen = _ogretmenService.GetByIdAsync(id);
                if (ogretmen == null)
                    return NotFound(new { message = $"ID {id} ile öğretmen bulunamadı" });

                _ogretmenService.Delete(id);
                return NoContent(); // 204 - Başarıyla silindi
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Öğretmen silinirken hata: {ex.Message}" });
            }
        }
    }
}
