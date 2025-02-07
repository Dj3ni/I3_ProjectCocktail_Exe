using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Comment
{
	public class CommentEditForm
	{
		[Required(ErrorMessage = "The field Title is compulsory")]
		[DisplayName("Comment's title: ")]
		[MaxLength(64, ErrorMessage = "Firstname field has a max size of 64 characters")]
		[MinLength(2, ErrorMessage = "Firstname field has a min size of 2 characters")]
		public string Title { get; set; }


		[Required(ErrorMessage = "The field Content")]
		[DisplayName("Content: ")]
		public string Content { get; set; }


		[DisplayName("Cocktail note (between 0 and 5): ")]
		[Range(0, 6, ErrorMessage = "La note doit avoir une valeur entre 0 et 5")]
		public short? Note { get; set; }
	}
}
