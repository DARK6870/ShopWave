using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.CartPage.Commands;
using ShopWave.Pages.ProductPage.Queryes;
using ShopWave.Pages.SupportPage.Commands;
using System.Security.Claims;

namespace ShopWave.Pages.ProductPage
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> details(int id)
        {
            try
            {
                Product? product = await _mediator.Send(new GetProductByIdQuery(id));

                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> products(string name, string category)
        {
            try
            {
                var products = await _mediator.Send(new GetAllProductsQuery());

                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(category))
                {
                    products = products.Where(p => p.Categoriess.CategoryName == category).ToList();
                }


                return View(products);
            }
            catch
            {
                return Redirect("/Error");
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int variationId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                Cart cart = new Cart
                {
                    AppUserId = userIdClaim.Value,
                    ProductId = productId,
                    VariationId = variationId
                };

                bool result = await _mediator.Send(new AddItemToCartCommand(cart));

                if (result)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false });
                }
            }
            catch
            {
                return Json(new { success = false });
            }
        }


    }
}
