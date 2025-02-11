using ASP_MVC.Models.Comment;
using BLL.Entities;

namespace ASP_MVC.Mappers
{
	internal static class CommentMapper
	{
		// BLL to ListItem
		public static CommentListItem ToListItem(this Comment comment)
		{
			if (comment == null) throw new ArgumentNullException(nameof(comment));

			return new CommentListItem()
			{
				//Comment_Id = comment.Comment_Id,
				Title = comment.Title,
				Content = comment.Content,
				Creator = (comment.Creator is null) ? "Ghost" : $"{comment.Creator.First_Name} {comment.Creator.Last_Name}",
				CreatedBy = comment.CreatedBy,
				Note = comment.Note,
			};
		}

		// BLL to Details
		public static CommentDetails ToDetails(this Comment comment)
		{
			if (comment == null) throw new ArgumentNullException(nameof(comment));

			return new CommentDetails()
			{
				Title = comment.Title,
				Content = comment.Content,
				CreatedBy = (comment.CreatedBy is null)? null: comment.CreatedBy,
				Creator = (comment.Creator is null) ? "Ghost" : $"{comment.Creator.First_Name} {comment.Creator.Last_Name}",
				CreatedAt = comment.CreatedAt,
				Note = comment.Note,
			};
		}

		// Convert Createform data to Bll data
		public static Comment ToBLL(this CommentCreateForm form)
		{
			if (form == null) throw new ArgumentNullException(nameof(form));
			return new Comment(
				Guid.Empty,
				form.Title,
				form.Content,
				form.Cocktail,
				DateTime.Now,
				(form.CreatedBy is null)? null : form.CreatedBy,
				(form.Note is null) ? null : form.Note
				);
		}

		// Convert BLL to ASP Data for Update

		public static CommentEditForm ToEditForm(this Comment comment)
		{
			if (comment == null) throw new ArgumentNullException(nameof(comment));
			return new CommentEditForm()
			{
				Title = comment.Title,
				Content = comment.Content,
				Note = comment.Note,
			};
		}

		////Convert EditForm data to Bll
		public static Comment ToBLL(this CommentEditForm form)
		{
			if (form == null) throw new ArgumentNullException(nameof(form));
			return new Comment(
				Guid.Empty,
				form.Title,
				form.Content,
				Guid.Empty,
				DateTime.Now,
				Guid.Empty,
				form.Note
			);
		}

		//// Convert BLL data to Delete form data
		public static CommentDelete ToDeleteForm(this Comment comment)
		{
			if (comment == null) throw new ArgumentNullException(nameof(comment));
			return new CommentDelete()
			{
				Title = comment.Title,
				Concern = comment.Concern,
				Content = comment.Content,
			};
		}
	}
}
