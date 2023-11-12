using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.SellerHub.SellerProduct.Queryes;

namespace ShopWave.Pages.SellerHub
{
    [Authorize(Roles = "Seller")]
    public class sellerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _usermanager;

        public sellerController(IMediator mediator, UserManager<IdentityUser> usermanager)
        {
            _mediator = mediator;
            _usermanager = usermanager;
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
    }
}
