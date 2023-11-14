using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.SellerHub.SellerOrders.Queryes;
using ShopWave.Pages.SellerHub.SellerOrders.ViewModels;
using ShopWave.Pages.SellerHub.SellerProduct.Queryes;

namespace ShopWave.Pages.SellerHub
{
    //[Authorize(Roles = "Seller")]
    public class sellerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _usermanager;

        public sellerController(IMediator mediator, UserManager<IdentityUser> usermanager)
        {
            _mediator = mediator;
            _usermanager = usermanager;
        }

        public async Task<bool> validateSeller(string userId)
        {
            var currentuser = await _usermanager.GetUserAsync(User);

            return currentuser.Id == userId;
        }

        public IActionResult panel()
        {
            return View();
        }

        public async Task<IActionResult> myproducts()
        {
            try
            {
                var user = await _usermanager.GetUserAsync(User);
                List<Product> products = await _mediator.Send(new GetProductsBySellerIdQuery(user.Id));

                return View(products);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> manageOrders()
        {
            try
            {
                var user = await _usermanager.GetUserAsync(User);
                List<SellerOrderViewModel> orders = await _mediator.Send(new GetOrdersBySellerQuery(user.Id));
                return View(orders);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        //public async Task<IActionResult> order(int id)
        //{
        //    try
        //    {
        //        SellerOrderViewModel order = await _mediator.Send(new GetOrderByIdQuery(id));
        //        bool valid;
        //    }
        //    catch
        //    {

        //    }
        //}
    }
}
