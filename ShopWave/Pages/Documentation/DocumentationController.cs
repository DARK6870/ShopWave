using Microsoft.AspNetCore.Mvc;

namespace ShopWave.Pages.Documentation
{
    public class DocumentationController : Controller
    {
        public IActionResult about_us()
        {
            return View();
        }

    }
}
