using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.OzelDersDtos;
using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Services;

namespace OzgurSeyhanWebSitesi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OzelDersController : ControllerBase
    {
        private readonly IOzelDersService _ozelDersService;
        private readonly IMapper _mapper;

        public OzelDersController(IOzelDersService ozelDersService, IMapper mapper)
        {
            _ozelDersService = ozelDersService;
            _mapper = mapper;
        }

        /// <summary>
        /// Tüm özel dersleri getir
        /// </summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var ozelDersler = _ozelDersService.GetAll();
                var ozelDersDtos = _mapper.Map<List<OzelDersDto>>(ozelDersler);
                return Ok(ozelDersDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Özel dersler getirilirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// ID'ye göre özel ders getir
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var ozelDers = _ozelDersService.GetByIdAsync(id);
                if (ozelDers == null)
                    return NotFound(new { message = $"ID {id} ile özel ders bulunamadı" });

                var ozelDersDto = _mapper.Map<OzelDersDto>(ozelDers);
                return Ok(ozelDersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Özel ders getirilirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Yeni özel ders oluştur
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOzelDersDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var ozelDers = _mapper.Map<OzelDers>(createDto);

                await _ozelDersService.AddAsync(ozelDers);

                var ozelDersDto = _mapper.Map<OzelDersDto>(ozelDers);
                return CreatedAtAction(nameof(GetById), new { id = ozelDers.Id }, ozelDersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Özel ders oluşturulurken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Özel ders güncelle
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateOzelDersDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != updateDto.Id)
                    return BadRequest(new { message = "URL'deki ID ile body'deki ID eşleşmiyor" });

                var existingOzelDers = _ozelDersService.GetByIdAsync(id);
                if (existingOzelDers == null)
                    return NotFound(new { message = $"ID {id} ile özel ders bulunamadı" });

                // Map ile güncelle
                _mapper.Map(updateDto, existingOzelDers);

                _ozelDersService.Update(existingOzelDers);

                var ozelDersDto = _mapper.Map<OzelDersDto>(existingOzelDers);
                return Ok(ozelDersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Özel ders güncellenirken hata: {ex.Message}" });
            }
        }

        /// <summary>
        /// Özel ders sil
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ozelDers = _ozelDersService.GetByIdAsync(id);
                if (ozelDers == null)
                    return NotFound(new { message = $"ID {id} ile özel ders bulunamadı" });

                _ozelDersService.Delete(id);
                return NoContent(); // 204 - Başarıyla silindi
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Özel ders silinirken hata: {ex.Message}" });
            }
        }
    }
}
