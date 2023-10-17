using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Models;
using ShopWave.Pages.ProductPage.Queryes;
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
            try
            {
                var user = await _context.Avatars.FirstOrDefaultAsync(p => p.AppUserId == userId);
                string? avatarBase64 = user.Data;
                return avatarBase64;
            }
            catch
            {
                return "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";
            }
        }

    }
}