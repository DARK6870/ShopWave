using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShopWave.Entity;
using ShopWave.Pages.AdminCategoryPage.Queryes;
using ShopWave.Pages.CartPage.Commands;
using ShopWave.Pages.ProductPage.Queryes;
using ShopWave.Pages.ProductPage.ViewModels;
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

        public async Task<IActionResult> products(string name, List<string> category, string sort, int page)
        {
            ViewBag.name = name;
            ViewBag.Selectedcategoryes = category;
            ViewBag.sort = sort;
            try
            {
                var product = await _mediator.Send(new GetAllProductsQuery());

                if (!string.IsNullOrEmpty(name))
                {
                    product = product.Where(p => p.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (category != null)
                {
                    foreach(var cat in category)
                    {
                        if (!string.IsNullOrEmpty(cat))
                        {
                            product = product.Where(p => p.Categoriess.CategoryName == cat).ToList();
                        }
                    }
                }

                if (sort != null)
                {
                    if (sort == "L")
                    {
                        product = product.OrderBy(p => p.ProductVariations.FirstOrDefault().price).ToList();
                    }
                    else if (sort == "H")
                    {
                        product = product.OrderByDescending(p => p.ProductVariations.FirstOrDefault().price).ToList();
                    }
                    else if (sort == "newest")
                    {
                        product = product.OrderBy(p => p.ProductDate).ToList();
                    }
                    else if (sort == "A-Z")
                    {
                        product = product.OrderBy(p => p.ProductName).ToList();
                    }
                    else if (sort == "Z-A")
                    {
                        product = product.OrderByDescending(p => p.ProductName).ToList();
                    }
                }

                var productmodel = new ProductViewModel()
                {
                    products = product,
                    categories = await _mediator.Send(new GetAllCategoryesQuery())
                };


                return View(productmodel);
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
