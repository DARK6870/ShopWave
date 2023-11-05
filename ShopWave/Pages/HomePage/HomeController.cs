using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Models;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.ProductPage.Queryes;
using ShopWave.Pages.UserAvatarPage.Queryes;
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
            try
            {
            var products = await _mediator.Send(new GetAllProductsQuery());
            products = products.OrderByDescending(p => p.ProductDate).Take(24).ToList();

            return View(products);
            }
            catch
            {
                return Redirect("/Error");
            }
            
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

        public async Task<string?> GetAvatar(string? userId)
        {
            string avatar = await _mediator.Send(new GetUserAvatarByIdQuery(userId));

            return avatar;
        }

    }
}