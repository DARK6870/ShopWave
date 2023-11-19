using Microsoft.AspNetCore.Mvc;

namespace ShopWave.Pages.Documentation
{
    public class DocumentationController : Controller
    {
        public IActionResult about_us()
        {
            return View();
        }
        
        public IActionResult how_to_become_a_seller()
        {
            return View();
        }

    }
}
