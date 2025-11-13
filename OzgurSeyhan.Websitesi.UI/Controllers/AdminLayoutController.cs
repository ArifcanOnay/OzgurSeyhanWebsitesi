using Microsoft.AspNetCore.Mvc;

namespace OzgurSeyhan.Websitesi.UI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
