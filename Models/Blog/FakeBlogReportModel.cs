using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBlog.Models.Blog
{
	public class FakeBlogReportModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ReportId { get; set; }

		[Required]
		[Display(Name = "Blog Id")]
		public int BlogId { get; set; }

		[Required]
		[Display(Name = "Blog Title")]
		[DataType(DataType.Text)]
		public string BlogTitle { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Blog Content")]
		[DataType(DataType.Text)]
		public string BlogContent { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Reason to report")]
		[DataType(DataType.Text)]
		public string Reason { get; set; } = string.Empty;
	}
}
