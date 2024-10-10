using FakeBlog.Models.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FakeBlog.Models
{
    public class FakeBlogDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
	{
		public DbSet<FakeBlogModel> Blogs { get; set; }

		public FakeBlogDbContext(DbContextOptions<FakeBlogDbContext> options)
			:base(options)
		{
			//
		}
	}
}
