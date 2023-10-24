﻿using MediatR;
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


        [Authorize]
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

        [Authorize]
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
                {   TempData["Error"] = true;
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


        [HttpPost]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                var fileExtension = Path.GetExtension(file.FileName);

                if (allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    var userId = currentUser.Id;

                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);
                        var avatar = new Avatar
                        {
                            AppUserId = userId,
                            Data = Convert.ToBase64String(stream.ToArray())
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
                }
                else
                {
                    TempData["Error"] = true;
                    return Redirect("/profile");
                }
            }
            return Redirect("/profile");
        }

    }
}
