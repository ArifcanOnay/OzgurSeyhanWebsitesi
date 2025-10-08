using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    public class OnceSonraController : Controller
    {
        private readonly IOnceSonraService _onceSonraService;

        public OnceSonraController(IOnceSonraService onceSonraService)
        {
            _onceSonraService = onceSonraService;
        }

        public async Task<IActionResult> Index()
        {
            var onceSonralar = await _onceSonraService.GetAktifOnceSonralarAsync();
            return View(onceSonralar);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var onceSonra = await _onceSonraService.GetOnceSonraByIdAsync(id);

            if (onceSonra == null)
            {
                return NotFound();
            }

            return View(onceSonra);
        }
    }
}

