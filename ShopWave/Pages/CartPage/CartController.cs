using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Pages.CartPage.Commands;
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

        [Authorize]
        public async Task<IActionResult> deletefromcart(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                bool res = await _mediator.Send(new DeteleItemFromCartCommand(id, user.Id));
                if (res)
                {
                    return Redirect("/mycart");
                }
                else
                {
                    return Redirect("/Error");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }
    }
}
