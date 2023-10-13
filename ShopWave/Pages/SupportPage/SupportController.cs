using Microsoft.AspNetCore.Mvc;

namespace ShopWave.Pages.SupportPage
{
    public class SupportController : Controller
    {
        public IActionResult support()
        {
            return View();
        }
    }
}
