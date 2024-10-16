using FakeBlog.Models.Account;
using FakeBlog.Models.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeBlog.Models
{
    public class FakeBlogDbContext : IdentityDbContext<FakeBlogUserModel, IdentityRole, string>
	{
		public DbSet<FakeBlogModel> Blogs { get; set; }

		public FakeBlogDbContext(DbContextOptions<FakeBlogDbContext> options)
			: base(options)
		{
			//
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//Create master user
			FakeBlogUserModel masterUser = new() { Id = "1", UserName = "Master", NormalizedUserName = "MASTER", LockoutEnabled = false };
			PasswordHasher<FakeBlogUserModel> passwordHasher = new();
			masterUser.PasswordHash = passwordHasher.HashPassword(masterUser, "Master123.");
			builder.Entity<FakeBlogUserModel>().HasData(masterUser);

			//Create master role
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole { Id = "1", Name = "Master", NormalizedName = "MASTER" });

			//Put master using in role
			builder.Entity<IdentityUserRole<string>>().HasData(
				new IdentityUserRole<string> { RoleId = "1", UserId = "1" });
		}
	}
}