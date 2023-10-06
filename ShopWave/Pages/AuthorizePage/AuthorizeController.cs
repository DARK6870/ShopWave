using Microsoft.AspNetCore.Mvc;

namespace ShopWave.Pages.AuthorizePage
{
	public class AuthorizeController : Controller
	{
		public IActionResult login()
		{
			return View();
		}

		public IActionResult reg()
		{
			return Redirect("/login");
		}
	}
}
