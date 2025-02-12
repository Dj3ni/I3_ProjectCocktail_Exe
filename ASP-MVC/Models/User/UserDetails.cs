using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ASP_MVC.Models.Cocktail;
using ASP_MVC.Models.Comment;

namespace ASP_MVC.Models.User
{
	public class UserDetails
	{
		[ScaffoldColumn(false)]
		public Guid User_Id { get; set; }

		[DisplayName("Firstname: ")]
		public string First_Name { get; set; }

		[DisplayName("Lastname: ")]
		public string Last_Name { get; set; }

		[DisplayName("Email: ")]
		public string Email { get; set; }


		[DisplayName("Subscription date: ")]
		public DateOnly CreatedAt { get; set; }

		[DisplayName("Shared cocktails: ")]
		public IEnumerable<CocktailListItem> Cocktails { get; set; } // We already created a, short model for cocktails so we can reuse it

		[DisplayName("My comments: ")]
		public IEnumerable<CommentListMin> Comments { get; set; }
	}
}
