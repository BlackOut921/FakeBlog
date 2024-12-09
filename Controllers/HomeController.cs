using System.Linq;

using FakeBlog.Models;
using FakeBlog.Models.Account;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FakeBlog.Controllers
{
	[AllowAnonymous]
	public class HomeController(
		FakeBlogDbContext _fakeBlogDbContext, 
		UserManager<FakeBlogUserModel> _userManager) : Controller
	{
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;
		private readonly UserManager<FakeBlogUserModel> userManager = _userManager;

		[HttpGet]
		public IActionResult Index()
		{
			FakeBlogSearchResult result = new();

			//Get recent blog posts (last 2 months)
			result.Posts = fakeBlogDbContext.Blogs
				.Where(i => i.LastUpdated.Month == DateTime.Now.Month || i.LastUpdated.Month == DateTime.Now.Month - 1)
				.OrderBy(i => i.LastUpdated).Reverse();

			return View(result);
		}

		public IActionResult Search(FakeBlogSearchResult model)
		{
			FakeBlogSearchResult result = new();

			result.Query = model.Query;

			result.Posts = fakeBlogDbContext.Blogs
				.Where(i => i.Title.Contains(model.Query) || i.Content.Contains(model.Query))
				.OrderBy(i => i.LastUpdated).Reverse();

			result.Users = userManager.Users
				.Where(i => i.UserName!.Contains(model.Query))
				.OrderBy(i => i.UserName);

			return View(result);
		}
	}
}