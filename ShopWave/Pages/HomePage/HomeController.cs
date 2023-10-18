using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Models;
using ShopWave.Pages.ProductPage.Queryes;
using ShopWave.Pages.UserAvatarPage.Queryes;
using System.Diagnostics;

namespace ShopWave.Pages.HomePage
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly AppDBContext _context;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, AppDBContext context)
        {
            _logger = logger;
            _mediator = mediator;
           _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
            var res = await _mediator.Send(new GetAllProductsQuery());
            return View(res);
            }
            catch
            {
                return NotFound();
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
            var users = await _mediator.Send(new GetAllAvatarsQuery());

            var user = users.FirstOrDefault(p => p.AppUserId == userId);
            return user?.Data;
        }

    }
}