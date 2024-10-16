using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using FakeBlog.Models.Account;
using FakeBlog.Models;
using FakeBlog.Models.Blog;

namespace FakeBlog.Controllers
{
    public class AccountController(
		SignInManager<FakeBlogUserModel> _signInManager, 
		UserManager<FakeBlogUserModel> _userManager,
		FakeBlogDbContext _fakeBlogDbContext) : Controller
	{
		private readonly SignInManager<FakeBlogUserModel> signInManager = _signInManager;
		private readonly UserManager<FakeBlogUserModel> userManager = _userManager;
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index(string id)
		{
			//Find user by id first then by username if null
			FakeBlogUserModel? userProfile = await userManager.FindByIdAsync(id);
			if (userProfile == null)
				userProfile = await userManager.FindByNameAsync(id);

			if(userProfile != null)
			{
				//Get user blogs
				userProfile.Blogs = fakeBlogDbContext.Blogs.Where(i => i.AuthorId == userProfile.Id);
			}

			return View(userProfile);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login() => 
			View();

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
		public IActionResult Register() => 
			View();

		[HttpPost]
		public async Task<IActionResult> Register(FakeBlogUserRegisterModel model)
		{
			if(ModelState.IsValid)
			{
				FakeBlogUserModel userModel = new() { UserName = model.Username };
				PasswordHasher<FakeBlogUserModel> hasher = new();
				userModel.PasswordHash = hasher.HashPassword(userModel, model.Password);

				IdentityResult result = await userManager.CreateAsync(userModel, model.Password);
				if(result.Succeeded)
					return RedirectToAction("Login");

				foreach (IdentityError error in result.Errors)
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

		[HttpGet]
		[Authorize]
		public IActionResult Settings() => 
			View();
	}
}
