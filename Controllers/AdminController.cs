﻿using FakeBlog.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBlog.Controllers
{
	[Authorize(Roles = "Master")]
	public class AdminController(FakeBlogDbContext _fakeBlogDbContext) : Controller
	{
		private readonly FakeBlogDbContext fakeBlogDbContext = _fakeBlogDbContext;

		public IActionResult Index() => View();
	}
}
