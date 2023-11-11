using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.AdminHub.AdminSeller.Queryes;
using ShopWave.Pages.AdminHub.AdminSupport.Commands;
using ShopWave.Pages.AdminHub.AdminSupport.Queryes;

namespace ShopWave.Pages.AdminHub
{
    public class adminController : Controller
    {
        private readonly IMediator _mediator;

        public adminController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
