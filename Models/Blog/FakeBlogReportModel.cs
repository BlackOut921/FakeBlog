using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakeBlog.Models.Blog
{
	public class FakeBlogReportModel
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ReportId { get; set; }

		[Required]
		[Display(Name = "BlogId")]
		[DataType(DataType.Text)]
		public int BlogId { get; set; }

		[Display(Name = "Blog Title")]
		[DataType(DataType.Text)]
		public string BlogTitle { get; set; } = string.Empty;

		[Display(Name = "Blog Content")]
		[DataType(DataType.MultilineText)]
		public string BlogContent { get; set; } = string.Empty;

		[Required]
		[Display(Name = "Report Reason")]
		[DataType(DataType.MultilineText)]
		public string ReportReason { get; set; } = string.Empty;
	}
}
