using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASP_MVC.Models.Comment
{
	public class CommentListItem
	{
		[ScaffoldColumn(false)]
		public Guid Comment_Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Guid? CreatedBy { get; set; }
		public short? Note { get; set; }
	}
}
