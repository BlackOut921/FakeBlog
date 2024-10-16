using System.Threading.Tasks.Dataflow;

using FakeBlog.Models;
using FakeBlog.Models.Account;
using FakeBlog.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			FakeBlogUserAccountModel profile = new()
			{
				AuthorId = id,
				Blogs = fakeBlogDbContext.Blogs
					.Where(i => i.AuthorId == id)
					.OrderBy(i => i.LastUpdated)
			};

			return View(profile);
		}
	}
}
