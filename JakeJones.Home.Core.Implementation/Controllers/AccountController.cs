using System.Security.Claims;
using System.Threading.Tasks;
using JakeJones.Home.Core.Implementation.Models;
using JakeJones.Home.Core.Managers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JakeJones.Home.Core.Implementation.Controllers
{
	[Authorize] 
	public class AccountController : Controller
	{
		private readonly IUserManager _userManager;

		public AccountController(IUserManager userManager)
		{
			_userManager = userManager;
		}

		[Route("/login")]
		[AllowAnonymous]
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View("~/Views/Login/Index.cshtml");
		}

		[Route("/login")]
		[HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
		public async Task<IActionResult> LoginAsync(string returnUrl, LoginViewModel model)
		{
			ViewData["ReturnUrl"] = returnUrl;

			if (ModelState.IsValid && _userManager.ValidateUser(model.Username, model.Password))
			{
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(ClaimTypes.Name, model.Username));

				var principle = new ClaimsPrincipal(identity);

				var properties = new AuthenticationProperties
				{
					IsPersistent = model.RememberMe
				};

				await HttpContext.SignInAsync(principle, properties);

				return LocalRedirect(returnUrl ?? "/");
			}

			ModelState.AddModelError(string.Empty, "Username or password is invalid.");
			return View("~/Views/Login/Index.cshtml", model);
		}

		[Route("/logout")]
		public async Task<IActionResult> LogOutAsync()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return LocalRedirect("/");
		}
	}
}
