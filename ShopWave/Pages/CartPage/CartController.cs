using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.AdminCountryPage.Queryes;
using ShopWave.Pages.CartPage.Commands;
using ShopWave.Pages.CartPage.Models;
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
                List<Cart> res = await _mediator.Send(new GetAllCartQuery(user.Id));
                UserData? userdata = await _mediator.Send(new GetPostalDataByIdQuery(user.Id));

                CartViewModel cart = new CartViewModel()
                {
                    Carts = res
                };
                if (userdata != null)
                {
                    cart.CountryName = userdata.Countryess.CountryName;
                    cart.DeliveryPrice = userdata.Countryess.DeliveryPrice;
                }
                else
                {
                    cart.CountryName = "n";
                    cart.DeliveryPrice = 0;
                }
                return View(cart);
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
