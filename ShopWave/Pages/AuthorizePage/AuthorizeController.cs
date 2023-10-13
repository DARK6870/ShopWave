using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopWave.Pages.AuthorizePage.Models;
using System.Security.Claims;

namespace ShopWave.Pages.AuthorizePage
{
	public class AuthorizeController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public AuthorizeController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		public IActionResult login()
		{
			return View();
		}

		public IActionResult register()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{

				var user = new IdentityUser
				{
					UserName = model.Email,
					Email = model.Email
				};

				var result = await userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					await signInManager.SignInAsync(user, isPersistent: false);
                    TempData["ShowAlert"] = true;
                    return Redirect("/");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
            }
            TempData["ErrorAlert"] = true;
            return View(model);
		}


		public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Redirect("/login");
        }

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> login(string returnUrl = null)
		{
			LogInViewModel model = new LogInViewModel
			{
				ReturnUrl = returnUrl,
				ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
			};

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> login(LogInViewModel model, string returnUrl = null)
		{
			if (ModelState.IsValid)
			{
				var result = await signInManager.PasswordSignInAsync(model.Email,
					model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					{
						return Redirect(returnUrl);
					}
					else
					{
                        TempData["ShowAlert"] = true;
                        return RedirectToAction("Index", "Home");
					}
				}

				ModelState.AddModelError(string.Empty, "Email or Password is incorect");
			}

			model.ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            TempData["ErrorLogin"] = true;
            return View(model);
		}




		[AllowAnonymous]
		[HttpPost]
		public IActionResult ExternalLogin(string provider, string returnUrl)
		{
			var redirectUrl = Url.Action("ExternalLoginCallback", "Authorize",
									new { ReturnUrl = returnUrl });

			var properties =
				signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

			return new ChallengeResult(provider, properties);
		}



		[AllowAnonymous]
		public async Task<IActionResult>
			ExternalLoginCallback(string returnUrl = null, string remoteError = null)
		{
			returnUrl = returnUrl ?? Url.Content("~/");

			LogInViewModel loginViewModel = new LogInViewModel
			{
				ReturnUrl = returnUrl,
				ExternalLogins =
						(await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
			};

			if (remoteError != null)
			{
				ModelState
					.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

				return View("login", loginViewModel);
			}

			var info = await signInManager.GetExternalLoginInfoAsync();
			if (info == null)
			{
				ModelState
					.AddModelError(string.Empty, "Error loading external login information.");

				return View("login", loginViewModel);
			}


			var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider,
				info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

			if (signInResult.Succeeded)
			{
                TempData["ShowAlert"] = true;
                return LocalRedirect(returnUrl);
			}

			else
			{
				var email = info.Principal.FindFirstValue(ClaimTypes.Email);

				if (email != null)
				{
					var user = await userManager.FindByEmailAsync(email);

					if (user == null)
					{
						user = new IdentityUser
						{
							UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
							Email = info.Principal.FindFirstValue(ClaimTypes.Email)
						};

						await userManager.CreateAsync(user);
					}
					await userManager.AddLoginAsync(user, info);
					await signInManager.SignInAsync(user, isPersistent: false);

					return LocalRedirect(returnUrl);
				}

				ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
				ViewBag.ErrorMessage = "Please contact support on Pragim@PragimTech.com";

				return View("Error");
			}
		}
	}
}
