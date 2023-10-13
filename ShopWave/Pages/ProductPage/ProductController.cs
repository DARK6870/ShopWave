using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> products()
        {
            try
            {
                var products = await _mediator.Send(new GetAllProductsQuery());
                return View(products);
            }
            catch
            {
                return Redirect("/Error");
            }
        }
    }
}
