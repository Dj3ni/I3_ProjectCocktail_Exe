using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Cocktail
{
	public class CocktailListItemMin
	{
		[ScaffoldColumn(false)]
		public Guid Cocktail_Id { get; set; }
		[DisplayName("Cocktail : ")]
		public string Name { get; set; }
	}
}
