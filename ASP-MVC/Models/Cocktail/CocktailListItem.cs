using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Cocktail
{
	public class CocktailListItem
	{
		[ScaffoldColumn(false)]
		public Guid Cocktail_Id { get; set; }

		[DisplayName("Name: ")]
		public string Cocktail_Name { get; set; }

		[DisplayName("Description")]
		public string Cocktail_Description { get; set; }

		//[DisplayName("Instructions: ")]
		//public string Cocktail_Instructions { get; set; }
	}
}
