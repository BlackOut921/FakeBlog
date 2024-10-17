using FakeBlog.Models;
using FakeBlog.Models.Blog;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBlog.Controllers
{
	[AllowAnonymous]
	public class HomeController(FakeBlogDbContext _fakeBlogDbContext) : Controller
	{
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;

		[HttpGet]
		public IActionResult Index()
		{
			//Get recent blog posts (last 24hours??)
			IEnumerable<FakeBlogModel> recentBlogs = fakeBlogDbContext.Blogs
				.Where(i => i.LastUpdated.Month == DateTime.Now.Month)
				.OrderBy(i => i.LastUpdated);

			return View(recentBlogs);
		}
	}
}
