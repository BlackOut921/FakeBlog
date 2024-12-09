using System.ComponentModel;

using FakeBlog.Models.Account;
using FakeBlog.Models.Blog;

namespace FakeBlog.Models
{
	public class FakeBlogSearchResult
	{
		[DisplayName("Query")]
		public string Query { get; set; } = string.Empty;

		[DisplayName("Posts")]
		public IEnumerable<FakeBlogModel>? Posts { get; set; }

		[DisplayName("Users")]
		public IEnumerable<FakeBlogUserModel>? Users { get; set; }
	}
}
