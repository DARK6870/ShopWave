using Microsoft.AspNetCore.Mvc;

namespace ShopWave.Pages.AccountPage
{
	public class AccountController : Controller
	{
		public IActionResult profile()
		{
			return View();
		}
	}
}
