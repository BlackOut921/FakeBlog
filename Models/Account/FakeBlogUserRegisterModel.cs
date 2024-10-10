using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models.Account
{
    public class FakeBlogUserRegisterModel
    {
        [Required]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
