using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Comment
{
	public class CommentDetails
	{
		[ScaffoldColumn(false)]
		public Guid Comment_Id { get; set; }

		[DisplayName("Comment's title: ")]
		public string Title { get; set; }

		[DisplayName("Content: ")]
		public string Content { get; set; }

		[DisplayName("Cocktail concerned: ")]
		public Guid Concern { get; set; }

		[DisplayName("Commented on: ")]
		[DataType(DataType.Date)]
		public DateTime CreatedAt { get; set; }

		[DisplayName("From: ")]
		public string? Creator { get; set; }

		[ScaffoldColumn(false)]
		public Guid? CreatedBy { get; set; }

		[DisplayName("Cocktail note: ")]
		public short? Note { get; set; }
	}
}
