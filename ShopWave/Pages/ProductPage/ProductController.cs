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
using System.Drawing.Printing;
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

        public async Task<IActionResult> products(string name, string category, string sort, int page)
        {
            ViewBag.name = name;
            ViewBag.category = category;
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
                        if (!string.IsNullOrEmpty(category))
                        {
                            if (category == "all")
                            {

                            }
                            else
                            {
                                product = product.Where(p => p.Categoriess.CategoryName == category).ToList();
                            }
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
                    else if (sort == "order")
                    {
                        product = product.OrderByDescending(p => p.OrderCounts).ToList();
                    }
                }

                //pages
                int totalProducts = product.Count;
                int pagesize = 4;
                int skip = (page - 1) * pagesize;
                product = product.Skip(skip).Take(pagesize).ToList();

                
                int total = (int)Math.Ceiling((double)totalProducts / pagesize);


                var productmodel = new ProductViewModel()
                {
                    products = product,
                    categories = await _mediator.Send(new GetAllCategoryesQuery()),
                    page = page,
                    totalPages = total
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
