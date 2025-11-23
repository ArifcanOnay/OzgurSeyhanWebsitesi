using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Core.Dtos.OzelDersDtos;
using System.Text;
using System.Text.Json;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class OzelDersController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl = "http://localhost:5246/api";

        public OzelDersController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Özel Ders Listesi
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/OzelDers");

            if (response.IsSuccessStatusCode)
            {
                var ozelDersler = await response.Content.ReadFromJsonAsync<List<OzelDersDto>>();
                return View(ozelDersler);
            }

            return View(new List<OzelDersDto>());
        }

        // Özel Ders Ekleme Sayfası
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Özel Ders Ekleme İşlemi
        [HttpPost]
        public async Task<IActionResult> Create(CreateOzelDersDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createDto);
            }

            // Sabit öğretmen ID'yi ata
            createDto.OgretmenId = 1;

            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(createDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_apiBaseUrl}/OzelDers", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Özel ders başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            var error = await response.Content.ReadAsStringAsync();
            TempData["ErrorMessage"] = $"Hata: {error}";
            return View(createDto);
        }

        // Özel Ders Güncelleme Sayfası
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}/OzelDers/{id}");

            if (response.IsSuccessStatusCode)
            {
                var ozelDers = await response.Content.ReadFromJsonAsync<OzelDersDto>();
                
                if (ozelDers == null)
                {
                    return RedirectToAction("Index");
                }

                var updateDto = new UpdateOzelDersDto
                {
                    Id = ozelDers.Id,
                    KurSeviyesi = ozelDers.KurSeviyesi,
                    Aciklama = ozelDers.Aciklama,
                    HaftalikSaat = ozelDers.HaftalikSaat,
                    MaksimumOgrenciSayisi = ozelDers.MaksimumOgrenciSayisi,
                    Gunler = ozelDers.Gunler,
                    SaatAraligi = ozelDers.SaatAraligi,
                    OgretmenId = ozelDers.OgretmenId
                };

                return View(updateDto);
            }

            return RedirectToAction("Index");
        }

        // Özel Ders Güncelleme İşlemi
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateOzelDersDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateDto);
            }

            // Sabit öğretmen ID'yi ata
            updateDto.OgretmenId = 1;

            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(updateDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_apiBaseUrl}/OzelDers/{updateDto.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Özel ders başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Özel ders güncellenirken hata oluştu!";
            return View(updateDto);
        }

        // Özel Ders Silme
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}/OzelDers/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Özel ders başarıyla silindi!";
            }
            else
            {
                TempData["ErrorMessage"] = "Özel ders silinirken hata oluştu!";
            }

            return RedirectToAction("Index");
        }
    }
}
