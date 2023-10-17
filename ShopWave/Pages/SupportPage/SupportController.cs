using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Entity;
using ShopWave.Pages.SupportPage.Commands;

namespace ShopWave.Pages.SupportPage
{
    public class SupportController : Controller
    {
        private readonly IMediator _mediator;
        public SupportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult support()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostSupport(Support support)
        {
            try
            {
                support.SupportDate = DateTime.Now;
                bool result = await _mediator.Send(new AddSupportCommand(support));

                if (result)
                {
                    TempData["Success"] = true;
                }
                else
                {
                    TempData["Error"] = true;
                }
            }
            catch
            {
                TempData["Error"] = true;
            }

            return Redirect("/support");
        }

    }
}
