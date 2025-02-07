using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Comment
{
	public class CommentDelete
	{
		
		[DisplayName("Comment's title: ")]
		public string Title { get; set; }

		[DisplayName("Cocktail concerned: ")]
		public Guid Concern { get; set; }

		[DisplayName("Content: ")]
		public string Content { get; set; }

	}
}
