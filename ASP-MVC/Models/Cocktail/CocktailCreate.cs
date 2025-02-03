using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Cocktail
{
	public class CocktailCreate
	{

		[Required (ErrorMessage = "The field Name is compulsory")]
		[DisplayName("Title: ")]
		[MaxLength(64, ErrorMessage = "Name field has a max size of 64 characters")]
		[MinLength(2, ErrorMessage = "Name field has a min size of 2 characters")]
		public string Cocktail_Name { get; set; }

		[DisplayName("Description: ")]
		[MaxLength(512,ErrorMessage ="Description field has a max size of 512 characters")]
		[DataType(DataType.Text)]
		public string? Cocktail_Description { get; set; }

		[Required(ErrorMessage = "The field Instructions is compulsory")]
		[DisplayName("Instructions: ")]
		[MinLength(2, ErrorMessage = "Name field has a min size of 2 characters")]
		[DataType(DataType.Text)]
		public string Cocktail_Instructions { get; set; }

		[Required(ErrorMessage ="You have to choose a Creator for this recipe")]
		[DisplayName("Author")]
		public Guid? Cocktail_Author { get; set; }
	}
}
