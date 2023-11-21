using LazyCache;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Context;
using ShopWave.Entity;
using ShopWave.Pages.AdminHub.AdminProduct.Commands;
using ShopWave.Pages.AdminHub.AdminSeller.Commands;
using ShopWave.Pages.AdminHub.AdminSeller.Queryes;
using ShopWave.Pages.AdminHub.AdminSupport.Commands;
using ShopWave.Pages.AdminHub.AdminSupport.Queryes;
using ShopWave.Pages.ProductPage.Queryes;

namespace ShopWave.Pages.AdminHub
{
    [Authorize(Roles = "Admin")]
    public class adminController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;

        public adminController(IMediator mediator, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, AppDBContext context, IAppCache cache)
        {
            _mediator = mediator;
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _cache = cache;
        }


        public IActionResult dashboard()
        {
            return View();
        }

        public async Task<IActionResult> managesupport()
        {
            try
            {
                List<Support> support = await _mediator.Send(new GetAllSupportQuery());
                return View(support);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> deletesupport(int id)
        {
            try
            {
                bool result = await _mediator.Send(new DeleteSupportCommand(id));

                if (result)
                {
                    TempData["Success"] = true;
                    return Redirect("/admin/managesupport");
                }
                else
                {
                    TempData["Error"] = true;
                    return Redirect("/admin/managesupport");
                }
            }
            catch
            {
                TempData["Error"] = true;
                return Redirect("/admin/managesupport");
            }
        }

        public async Task<IActionResult> sellerrequests()
        {
            try
            {
                List<SellerData> sellers = await _mediator.Send(new GetAllSellerRequestsQuery());
                sellers = sellers.Where(p => p.admitered == false).ToList();
                return View(sellers);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> seller(int id)
        {
            try
            {
                SellerData seller = await _mediator.Send(new GetSellerByIdQuery(id));
                return View(seller);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> AssignRole(int id)
        {
            try
            {
                SellerData seller = await _mediator.Send(new GetSellerByIdQuery(id));

                string roleName = "Seller";
                var user = await _userManager.FindByIdAsync(seller.AppUserId);

                if (user == null)
                {
                    return NotFound();
                }

                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }

                await _userManager.AddToRoleAsync(user, roleName);

                seller.admitered = true;
                await _context.SaveChangesAsync();

                return Redirect("/admin/sellerrequests");
            }
            catch
            {
                return Redirect("/Error");
            }
        }


        public async Task<IActionResult> DeleteSellerRequest(int id)
        {
            try
            {
                bool res = await _mediator.Send(new DeleteSellerDataCommand(id));
                if (res)
                {
                    return Redirect("/admin/sellerrequests");
                }
                return Redirect("/Error");
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> productadministration()
        {
            try
            {
                List<Product> products = await _mediator.Send(new GetNonAdmiteredProductsQuery());
                return View(products);
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> acceptproduct(int id)
        {
            try
            {
                bool res = await _mediator.Send(new AcceptProductCommand(id));
                return Redirect("/admin/productadministration");
            }
            catch
            {
                return Redirect("/Error");
            }
        }

        public async Task<IActionResult> declineproduct(int id)
        {
            try
            {
                bool res = await _mediator.Send(new DeclineProductCommand(id));
                return Redirect("/admin/productadministration");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
        
        public async Task<IActionResult> removecache()
        {
            try
            {
                _cache.Remove("products_data");
                return Redirect("/admin/dashboard");
            }
            catch
            {
                return Redirect("/Error");
            }
        }
    }
}
