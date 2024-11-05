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
		public IActionResult Create() => 
			View();

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
				model.LastUpdated = model.DateCreated = DateTime.Now;

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
		public IActionResult Read(int id) =>
			View(fakeBlogDbContext.Find<FakeBlogModel>(id));

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

		[HttpGet]
		[Authorize]
		public IActionResult Edit(int id) =>
			View(fakeBlogDbContext.Find<FakeBlogModel>(id));

		[HttpPost]
		[Authorize]
		public IActionResult Edit(FakeBlogModel model)
		{
			if(ModelState.IsValid)
			{
				//Update
				fakeBlogDbContext.Update<FakeBlogModel>(model);
				fakeBlogDbContext.SaveChanges();
				return RedirectToAction("Index");

				/*//Get blog from database
				FakeBlogModel? blogToUpdate = fakeBlogDbContext.Find<FakeBlogModel>(model.BlogId);
				if(blogToUpdate != null)
				{
					//Update required fields
					blogToUpdate.Title = model.Title;
					blogToUpdate.Content = model.Content;
					blogToUpdate.LastUpdated = DateTime.Now;
					blogToUpdate.Anonymous = model.Anonymous;

					//Save database
					fakeBlogDbContext.SaveChanges();

					//Unknown error atm
					return RedirectToAction("Index");
				}*/
			}

			ModelState.AddModelError(string.Empty, "error");
			return View(model);
		}

		[HttpGet]
		public IActionResult Report(int id)
		{
			FakeBlogModel? blog = fakeBlogDbContext.Find<FakeBlogModel>(id);
			if(blog != null)
			{
				FakeBlogReportModel newReport = new()
				{
					BlogId = blog.BlogId,
					BlogTitle = blog.Title,
					BlogContent = blog.Content,
					ReportReason = string.Empty
				};

				return View(newReport);
			}

			return View();
		}

		[HttpPost]
		public IActionResult Report(FakeBlogReportModel model)
		{
			if (ModelState.IsValid) 
			{
				fakeBlogDbContext.Add<FakeBlogReportModel>(model);
				fakeBlogDbContext.SaveChangesAsync();

				return RedirectToAction("Read", model.BlogId);
			}

			ModelState.AddModelError(string.Empty, "error");
			return View(model);
		}

		private void Join()
		{
			IEnumerable<FakeBlogModel> j = fakeBlogDbContext.Users.Join(
				fakeBlogDbContext.Blogs, //join Tabel1 data to targetTable
				user => user.Id, //the key column to check from Table1
				blog => blog.AuthorId, //the key column to check from Table2
				(user, blog) => new FakeBlogModel { AuthorId = user.Id, BlogId = blog.BlogId }); //the result to return
		}
	}
}
