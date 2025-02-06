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
				CreatedBy = comment.CreatedBy,
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
				Guid.Empty,
				DateTime.Now,
				null,
				(form.Note is null) ? null : form.Note
				);
		}

		// Convert BLL to ASP Data for Update

		public static CocktailEditForm EditForm(this Comment comment)
		{
			if (comment == null) throw new ArgumentNullException(nameof(comment));
			return new CocktailEditForm()
			{
				//Cocktail_Id = cocktail.Cocktail_Id,
				Cocktail_Name = comment.Name,
				Cocktail_Description = comment.Description,
				Cocktail_Instructions = comment.Instructions,
			};
		}

		//Convert EditForm data to Bll
		public static Cocktail ToBLL(this CocktailEditForm form)
		{
			if (form == null) throw new ArgumentNullException(nameof(form));
			return new Cocktail(
				form.CocktailId,
				form.Cocktail_Name,
				form.Cocktail_Instructions,
				form.Cocktail_Description
			);
		}

		// Convert BLL data to Delete form data
		public static CocktailDelete DeleteForm(this Cocktail cocktail)
		{
			if (cocktail == null) throw new ArgumentNullException(nameof(cocktail));
			return new CocktailDelete()
			{
				Cocktail_Name = cocktail.Name,
				Cocktail_Description = cocktail.Description,
			};
		}
	}
}
