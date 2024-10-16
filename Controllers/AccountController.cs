using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using FakeBlog.Models.Account;

namespace FakeBlog.Controllers
{
    public class AccountController(
		SignInManager<IdentityUser> _signInManager, 
		UserManager<IdentityUser> _userManager) : Controller
	{
		private readonly SignInManager<IdentityUser> signInManager = _signInManager;
		private readonly UserManager<IdentityUser> userManager = _userManager;

		[Authorize]
		public IActionResult Index() => View();

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login() => View();

		[HttpPost]
		public async Task<IActionResult> Login(FakeBlogUserLoginModel model)
		{
			if(ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(
					model.Username, model.Password, model.RememberMe, false);

				if (result.Succeeded)
					return RedirectToAction("Index", "Home");
			}

			ModelState.AddModelError(string.Empty, "Login error");
			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register() => View();

		[HttpPost]
		public async Task<IActionResult> Register(FakeBlogUserRegisterModel model)
		{
			if(ModelState.IsValid)
			{
				FakeBlogUserAccountModel userModel = new() { UserName = model.Username }; //New

				IdentityUser newUser = new() { UserName = model.Username };

				PasswordHasher<IdentityUser> hasher = new();
				newUser.PasswordHash = hasher.HashPassword(newUser, model.Password);

				IdentityResult result = await userManager.CreateAsync(newUser, model.Password);
				if(result.Succeeded)
				{
					return RedirectToAction("Login");
				}

				foreach(IdentityError error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

				return View(model);
			}

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
