using Microsoft.AspNetCore.Mvc;
using OzgurSeyhanWebSitesi.Data;
using OzgurSeyhanWebSitesi.Services;

namespace OzgurSeyhanWebSitesi.Controllers
{
    public class KursController : Controller
    {
        private readonly IKursService _kursService;

        public KursController(IKursService kursService)
        {
            _kursService = kursService;
        }

        public async Task<IActionResult> Index()
        {
            var kurslar = await _kursService.GetAktifKurslarAsync();
            return View(kurslar);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var viewModel = await _kursService.GetKursDetayAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }


}
