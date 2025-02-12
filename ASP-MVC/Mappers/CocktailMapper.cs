using ASP_MVC.Models.Cocktail;
using ASP_MVC.Models.User;
using BLL.Entities;

namespace ASP_MVC.Mappers
{
	internal static class CocktailMapper
	{
		// BLL to ListItem
		public static CocktailListItem ToListItem(this Cocktail cocktail)
		{
			if(cocktail == null)throw new ArgumentNullException(nameof(cocktail));

			return new CocktailListItem()
			{
				Cocktail_Id = cocktail.Cocktail_Id,
				Cocktail_Name = cocktail.Name,
				Cocktail_Description = (cocktail.Description is null) ? null : cocktail.Description
			};
		}

		// BLL to Details

		public static CocktailDetails ToDetails(this Cocktail cocktail)
		{
			if(cocktail == null)throw new ArgumentNullException(nameof(cocktail));

			return new CocktailDetails()
			{
				Cocktail_Name = cocktail.Name,
				Cocktail_Description = (cocktail.Description is null) ? null : cocktail.Description,
				Cocktail_Instructions = cocktail.Instructions,
				Cocktail_Id = cocktail.Cocktail_Id,
				CreatedAt = DateOnly.FromDateTime(cocktail.CreatedAt),
				Creator = (cocktail.Creator is null) ? null : $"{cocktail.Creator.First_Name} {cocktail.Creator.Last_Name}",
				Comments = cocktail.Comments.Select(bll => bll.ToListItem())
			};
		}

		// Convert form data to Bll data
		public static Cocktail ToBLL(this CocktailCreate form)
		{
			if(form == null)throw new ArgumentNullException(nameof(form));
			return new Cocktail(
				Guid.Empty,
				form.Cocktail_Name,
				form.Cocktail_Instructions,
				DateTime.Now,
				form.Cocktail_Description,
				(form.Cocktail_Author is null)? null: form.Cocktail_Author				
				);			
		}

		// Convert BLL to ASP Data for Update

		public static CocktailEditForm EditForm(this Cocktail cocktail)
		{
			if (cocktail == null) throw new ArgumentNullException(nameof(cocktail));
			return new CocktailEditForm()
			{
				//Cocktail_Id = cocktail.Cocktail_Id,
				Cocktail_Name = cocktail.Name,
				Cocktail_Description = cocktail.Description,
				Cocktail_Instructions = cocktail.Instructions,
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
			if(cocktail == null) throw new ArgumentNullException(nameof(cocktail));
			return new CocktailDelete()
			{
				Cocktail_Name = cocktail.Name,
				Cocktail_Description= cocktail.Description,
			};
		}

	}
}
