using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Cocktail
{
	public class CocktailDetails
	{
		[ScaffoldColumn(false)]
		public Guid Cocktail_Id { get; set; }
		
		[DisplayName("Title: ")]
		public string Cocktail_Name { get; set; }

		[DisplayName("Description: ")]
		public string? Cocktail_Description { get; set; }

		[DisplayName("Instructions: ")]
		public string Cocktail_Instructions { get;set; }

		[DisplayName("Created: ")]
		public DateOnly CreatedAt {  get; set; }

		[DisplayName("This recipe was shared by : ")]
		public string? Creator { get; set; }

		



	}
}
