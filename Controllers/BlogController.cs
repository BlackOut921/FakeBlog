using FakeBlog.Models;
using FakeBlog.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FakeBlog.Controllers
{
	public class BlogController(
		UserManager<IdentityUser> _userManager, 
		FakeBlogDbContext _fakeBlogDbContext) : Controller
	{
		private readonly UserManager<IdentityUser> userManager = _userManager;
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			//Get current user
			IdentityUser? currentUser = await userManager.GetUserAsync(User);

			if(currentUser != null)
			{
				//Get blogs with username/id
				IEnumerable<FakeBlogModel> blogs = fakeBlogDbContext.Blogs
					.Where(i => i.Author == currentUser.UserName && i.AuthorId == currentUser.Id);

				return View(blogs);
			}

			return View();
		}

		[Authorize]
		[HttpGet]
		public IActionResult Create() => View();

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Create(FakeBlogModel model)
		{
			if (ModelState.IsValid)
			{
				//Get current username and id
				IdentityUser? currentUser = await userManager.GetUserAsync(User);
				model.Author = currentUser?.UserName ?? "NULL";
				model.AuthorId = currentUser?.Id ?? "000";

				//Set blog create/lastUpdate time
				model.DateCreated = DateTime.Now;
				model.LastUpdated = DateTime.Now;

				//Add to blog table and save database
				await fakeBlogDbContext.AddAsync<FakeBlogModel>(model);
				await fakeBlogDbContext.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			ModelState.AddModelError(string.Empty, "Error creating new blog");
			return View(model);
		}

		[AllowAnonymous]
		[HttpGet]
		public IActionResult View(int id)
		{
			FakeBlogModel? blog = fakeBlogDbContext.Find<FakeBlogModel>(id);
			return View(blog);
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			FakeBlogModel? blogToDelete = fakeBlogDbContext.Find<FakeBlogModel>(id);
			if (blogToDelete != null)
			{
				fakeBlogDbContext.Remove<FakeBlogModel>(blogToDelete);
				fakeBlogDbContext.SaveChanges();

				return RedirectToAction("Index");
			}

			return View();
		}
	}
}
