using System.ComponentModel;

namespace ASP_MVC.Models.Cocktail
{
	public class CocktailDelete
	{
		[DisplayName("Title: ")]
		public string Cocktail_Name { get; set; }

		[DisplayName("Description: ")]
		public string? Cocktail_Description { get; set; }
	}
}
