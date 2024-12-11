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

		public bool HasResult
		{
			get
			{
				return 
					Query != string.Empty || 
					Posts?.Count() > 0 || 
					Users?.Count() > 0 ? 
					true : 
					false;
			}
		}
	}
}
