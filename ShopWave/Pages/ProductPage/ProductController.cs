using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.ProductPage.Queryes;

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

    }
}
