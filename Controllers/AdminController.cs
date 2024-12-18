﻿using FakeBlog.Models;
using FakeBlog.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FakeBlog.Controllers
{
	[Authorize(Roles = "Master")]
	public class AdminController(
		FakeBlogDbContext _fakeBlogDbContext, 
		UserManager<FakeBlogUserModel> _userManager) : Controller
	{
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;
		private readonly UserManager<FakeBlogUserModel> userManager = _userManager;

		public IActionResult Index() => 
			View();

		public IActionResult UserManager() => 
			View(this.userManager.Users.ToList());

		public async Task<IActionResult> EditUser(string username) =>
			View(await userManager.FindByNameAsync(username));
	}
}
