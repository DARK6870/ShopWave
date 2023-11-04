using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.AdminCountryPage.Queryes;
using ShopWave.Pages.CartPage.Queryes;
using ShopWave.Pages.OrderPage.Commands;
using ShopWave.Pages.OrderPage.Queryes;
using ShopWave.Pages.OrderPage.ViewModels;

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

                byte deliveryprice = userdata.Countryess.DeliveryPrice;

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

        public async Task<IActionResult> myorders(int page)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var orders = await _mediator.Send(new GetAllOrdersQuery(user.Id));

                int pagesize = 12;
                int count = orders.Count();
                int skip = (page - 1) * pagesize;
                orders = orders.Skip(skip).Take(pagesize).ToList();
                int total = (int)Math.Ceiling((double)count / pagesize);

                OrderViewModel order = new OrderViewModel()
                {
                    Order = orders,
                    page = page,
                    totalpages = total
                };


                return View(order);
            }
            catch
            {
                return Redirect("/Error");
            }
        }
    }
}
