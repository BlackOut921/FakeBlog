using FakeBlog.Models;
using FakeBlog.Models.Account;
using FakeBlog.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FakeBlog.Controllers
{
    public class BlogController(
		UserManager<FakeBlogUserModel> _userManager,
		FakeBlogDbContext _fakeBlogDbContext) : Controller
	{
		private readonly UserManager<FakeBlogUserModel> userManager = _userManager;
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			//Get current user
			FakeBlogUserModel? currentUser = await userManager.GetUserAsync(User);

			if (currentUser != null)
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
				//Get current
				FakeBlogUserModel? currentUser = await userManager.GetUserAsync(User);

				//username shouldnt be null, need account and log in to post anything
				model.Author = currentUser?.UserName ?? "null username";
				model.AuthorId = currentUser?.Id ?? "null id";

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
		public async Task<IActionResult> View(int id) =>
			View(await fakeBlogDbContext.FindAsync<FakeBlogModel>(id));

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			FakeBlogModel? blogToDelete = fakeBlogDbContext.Find<FakeBlogModel>(id);
			if (blogToDelete != null)
			{
				fakeBlogDbContext.Remove<FakeBlogModel>(blogToDelete);
				await fakeBlogDbContext.SaveChangesAsync();

				return RedirectToAction("Index");
			}

			return View();
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Edit(int id) =>
			View(await fakeBlogDbContext.FindAsync<FakeBlogModel>(id));

		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Edit(FakeBlogModel model)
		{
			if(ModelState.IsValid)
			{
				//Get blog from database
				FakeBlogModel? blogToUpdate = await fakeBlogDbContext.FindAsync<FakeBlogModel>(model.BlogId);
				if(blogToUpdate != null)
				{
					//Update required fields
					blogToUpdate.Title = model.Title;
					blogToUpdate.Content = model.Content;
					blogToUpdate.LastUpdated = DateTime.Now;
					blogToUpdate.Anonymous = model.Anonymous;

					//Save database
					await fakeBlogDbContext.SaveChangesAsync();

					//Unknown error atm
					return RedirectToAction("Index");
				}
			}

			ModelState.AddModelError(string.Empty, "error");
			return View(model);
		}

		[HttpGet]
		public IActionResult Profile(string id)
		{
			FakeBlogUserModel profile = new()
			{
				Blogs = fakeBlogDbContext.Blogs
					.Where(i => i.AuthorId == id)
					.OrderBy(i => i.LastUpdated)
			};

			return View(profile);
		}
	}
}
