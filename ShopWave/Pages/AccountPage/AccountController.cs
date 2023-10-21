using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Pages.AccountPage.Queryes;

namespace ShopWave.Pages.AccountPage
{
	public class AccountController : Controller
	{
		private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(IMediator mediator, UserManager<IdentityUser> userManager)
		{
			_mediator = mediator;
			_userManager = userManager;
		}


		[Authorize]
		public async Task<IActionResult> profile()
		{
			try
			{
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = currentUser.Id;
                var user = await _mediator.Send(new GetUserByIdQuery(userId));

                return View(user);
			}
			catch
			{
				return Redirect("/Error");
			}
		}
	}
}
