using System.ComponentModel.DataAnnotations;

namespace FakeBlog.Models.Account
{
	public class FakeBlogUpdatePasswordModel
	{
		[Required]
		[Display(Name = "Current Password")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; } = string.Empty;

		[Required]
		[Display(Name = "New Password")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Confirm New Password")]
		[DataType(DataType.Password)]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm new password")]
		public string ConfirmPassword { get; set;} = string.Empty;
	}
}
