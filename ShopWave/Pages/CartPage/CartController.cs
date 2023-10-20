using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Pages.CartPage.Queryes;

namespace ShopWave.Pages.CartPage
{
    public class CartController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> mycart()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var res = await _mediator.Send(new GetAllCartQuery(user.Id));
                return View(res);
            }
            catch
            {
                return Redirect("/Error");
            }
        }
    }
}
