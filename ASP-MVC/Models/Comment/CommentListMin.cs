using System.ComponentModel.DataAnnotations;

namespace ASP_MVC.Models.Comment
{
	public class CommentListMin
	{
		[ScaffoldColumn(false)]
		public Guid Comment_Id { get; set; }
		public string Title { get; set; }

		[ScaffoldColumn(false)]
		public Guid? Concern { get; set; }
		public string CocktailConcerned { get; set; }
		public string Content { get; set; }
		public short? Note { get; set; }
		//[ScaffoldColumn(false)]
		public Guid? CreatedBy { get; set; }
	}
}
