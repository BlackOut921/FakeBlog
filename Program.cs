using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using FakeBlog.Models;
using FakeBlog.Models.Account;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<FakeBlogDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<FakeBlogUserModel, IdentityRole>()
	.AddEntityFrameworkStores<FakeBlogDbContext>()
	.AddDefaultTokenProviders();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();
app.Run();