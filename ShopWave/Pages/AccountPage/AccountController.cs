using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopWave.Entity;
using ShopWave.Pages.AccountPage.Commands;
using ShopWave.Pages.AccountPage.Queryes;
using ShopWave.Pages.AccountPage.ViewModels;
using ShopWave.Pages.AdminCountryPage.Queryes;

namespace ShopWave.Pages.AccountPage
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<string?> ImageToBase64(IFormFile file)
        {
            string? res = null;
            if (file != null && file.Length > 0)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(file.FileName);

                if (allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        res = Convert.ToBase64String(stream.ToArray());
                    }
                }
            }
            return res;
        }


        public async Task<IActionResult> profile()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = currentUser.Id;
                var user = await _mediator.Send(new GetUserByIdQuery(userId));

                return View(user);
            }
            catch
            {
                return Redirect("/Error");
            }
        }


        public async Task<IActionResult> changeUsername(string username)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = currentUser.Id;

                bool result = await _mediator.Send(new ChangeUsernameCommand(userId, username));
                if (result)
                {
                    TempData["Success"] = true;
                    return Redirect("/profile");
                }
                else
                {
                    TempData["Error"] = true;
                    return Redirect("/profile");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }


        public async Task<IActionResult> userdata()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var userId = currentUser.Id;

                UserData? user = await _mediator.Send(new GetPostalDataByIdQuery(userId));

                var model = new UserDataViewModel
                {
                    countryes = await _mediator.Send(new GetCountryesQuery()),
                    UserDatas = user
                };

                return View(model);

            }
            catch
            {
                return Redirect("/Error");
            }

        }

        public async Task<IActionResult> updateuser(UserData data)
        {
            try
            {
                bool result = await _mediator.Send(new ChangeUserDataCommand(data));
                if (result)
                {
                    TempData["Success"] = true;
                    return Redirect("/profile");
                }
                else
                { TempData["Error"] = true;
                    return Redirect("/profile");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }




        public async Task<IActionResult> avatar()
        {
            return View();
        }

        public async Task<IActionResult> sellerdata()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> postSellerData(SellerData seller, IFormFile file)
        {
            try
            {
                var sellerdata = await _mediator.Send(new GetSellerDataByUserIdQuery(seller.AppUserId));
                if (sellerdata != null)
                {
                    TempData["Message"] = "You have already filled out an application";
                    return Redirect("/profile");
                }

                var img = await ImageToBase64(file);
                if (img != null)
                {
                    seller.Data = img;
                    bool result = await _mediator.Send(new PostSellerDataCommand(seller));
                    if (result)
                    {
                        TempData["Message"] = "Your request will be answered within 3 days";
                        return Redirect("/profile");
                    }
                    else
                    {
                        TempData["Error"] = true;
                        return Redirect("/profile");
                    }
                }
                else
                {
                    TempData["Error"] = true;
                    return Redirect("/profile");
                }
            }
            catch
            {
                return Redirect("/Error");
            }
        }



        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            try
            {
                if (file.Length > 2 * 1024 * 1024)
                {
                    TempData["Message"] = "File size is too big. You can try compressing the picture and try again.";
                    return Redirect("/avatar");
                }
                var img = await ImageToBase64(file);
                if (img != null)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    var userId = currentUser.Id;
                    var avatar = new Avatar
                    {
                        AppUserId = userId,
                        Data = img
                    };

                    bool result = await _mediator.Send(new ChangeAvatarCommand(avatar));
                    if (result)
                    {
                        TempData["Success"] = true;
                        return Redirect("/profile");
                    }
                    else
                    {
                        TempData["Error"] = true;
                        return Redirect("/profile");
                    }
                }
                else
                {
                    TempData["Error"] = true;
                    return Redirect("/profile");
                }
            }
            catch
            {
                TempData["Error"] = true;
                return Redirect("/profile");
            }
        }

    }
}
