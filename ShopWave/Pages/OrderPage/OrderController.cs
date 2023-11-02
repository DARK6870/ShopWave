using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.AdminCountryPage.Queryes;
using ShopWave.Pages.CartPage.Queryes;
using ShopWave.Pages.OrderPage.Commands;

namespace ShopWave.Pages.OrderPage
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<IActionResult> placeOrder()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var products = await _mediator.Send(new GetAllCartQuery(user.Id));

                var userdata = await _mediator.Send(new GetPostalDataByIdQuery(user.Id));
                if (userdata == null)
                {
                    TempData["Message"] = "Please fill out your shipping information to place your order.";
                    return Redirect("/profile");
                }

                var countryes = await _mediator.Send(new GetCountryesQuery());
                byte deliveryprice = countryes.FirstOrDefault(p => p.CountryId == userdata.CountryId).DeliveryPrice;

                short count = 0;

                foreach (var item in products)
                {
                    if (await _mediator.Send(new OrderProductCommand(item, deliveryprice)) == false)
                    {
                        count++;
                    }
                }

                if (count > 0)
                {
                    TempData["Message"] = $"Cannot order {count} products.";
                    return Redirect("/profile");
                }
                else
                {
                    TempData["Success"] = true;
                    return Redirect("/profile");
                }
            }
            catch
            {
                TempData["Error"] = true;
                return Redirect("/profile");
            }
        }
    }
}
