using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBlog.Models.Blog
{
	public class FakeBlogReportModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[Display(Name = "Blog Id")]
		public FakeBlogModel? Blog { get; set; } = null;

		[Required]
		[Display(Name = "Reason to report")]
		[DataType(DataType.Text)]
		public string Reason { get; set; } = string.Empty;
	}
}
