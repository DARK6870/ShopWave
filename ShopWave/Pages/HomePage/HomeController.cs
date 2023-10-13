using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Models;
using ShopWave.Pages.ProductPage.Queryes;
using System.Diagnostics;

namespace ShopWave.Pages.HomePage
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
                var res = await _mediator.Send(new GetAllProductsQuery());
                return View(res);
        }

        public IActionResult NotFound()
        {
            Response.StatusCode = 404;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}