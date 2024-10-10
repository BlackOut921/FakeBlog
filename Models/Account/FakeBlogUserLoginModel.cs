using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models.Account
{
    public class FakeBlogUserLoginModel
    {
        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; } = false;
    }
}
