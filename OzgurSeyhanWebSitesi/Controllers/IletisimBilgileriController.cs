using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Models;
using OzgurSeyhanWebSitesi.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    public class IletisimBilgileriController : Controller
    {
       
        private readonly IIletisimBilgisiService _iletisimBilgisiService;
        private readonly IOgretmenService _ogretmenService;

        public IletisimBilgileriController(
            IIletisimBilgisiService iletisimBilgisiService,
            IOgretmenService ogretmenService)
        {
            _iletisimBilgisiService = iletisimBilgisiService;
            _ogretmenService = ogretmenService;
        }

        // GET: IletisimBilgileri
        public async Task<IActionResult> Index()
        {
            var iletisimBilgisi = await _iletisimBilgisiService.GetAktifIletisimBilgisiAsync();
            
            // Eğer veritabanında iletişim bilgisi yoksa, Özgür Seyhan'ın bilgilerini oluştur
            if (iletisimBilgisi == null)
            {
                await CreateOzgurSeyhanIletisimBilgisi();
                iletisimBilgisi = await _iletisimBilgisiService.GetAktifIletisimBilgisiAsync();
            }
            
            return View(iletisimBilgisi);
        }

        private async Task CreateOzgurSeyhanIletisimBilgisi()
        {
            // Önce Özgür Seyhan öğretmenini bul
            var ozgurSeyhan = await _ogretmenService.GetOgretmenAsync();
            
            if (ozgurSeyhan != null)
            {
                var iletisimBilgisi = new IletisimBilgisi
                {
                    TelefonNumarasi = "05354893494",
                    Email = "ozgurseyhan@gmail.com",
                    YouTubeKanali = "https://www.youtube.com/@5dakikadaingilizce",
                    WhatsAppNumarasi = "",
                    Adres = "",
                    WebSitesi = "",
                    Aktif = true,
                    OgretmenId = ozgurSeyhan.Id
                };

                await _iletisimBilgisiService.CreateIletisimBilgisiAsync(iletisimBilgisi);
            }
        }
    }
}

