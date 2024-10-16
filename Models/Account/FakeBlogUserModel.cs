using System.ComponentModel.DataAnnotations;
using FakeBlog.Models.Blog;
using Microsoft.AspNetCore.Identity;

namespace FakeBlog.Models.Account
{
    public class FakeBlogUserModel : IdentityUser
    {
        [Display(Name = "Bio")]
        public string Bio { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Blogs")]
        public IEnumerable<FakeBlogModel>? Blogs { get; set; }

        /* Ideas: 
         * - Followed accounts (strings)
         */
    }
}
