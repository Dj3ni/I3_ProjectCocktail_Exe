using ASP_MVC.Models.Cocktail;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Xml.Linq;

namespace ASP_MVC.Handlers
{
	public class CocktailQueueCorrection
	{
		private readonly ISession _session;
		public CocktailQueueCorrection(IHttpContextAccessor accessor) 
		{
			_session = accessor.HttpContext.Session;
		}

		public IEnumerable<CocktailListItemMin> RecentlyVisitedCocktails
		{
			get
			{
				string? json = _session.GetString(nameof(RecentlyVisitedCocktails));
				if (json is null) return new CocktailListItemMin[0];
				return JsonSerializer.Deserialize<CocktailListItemMin[]>(json);
			}
			private set
			{
				string json = JsonSerializer.Serialize(value);
				_session.SetString(nameof(RecentlyVisitedCocktails), json);
			}
		}
		public void AddVisitedCocktail(CocktailListItemMin cocktail)
		{
			// Ligne permettant d'insérer le cocktail
			List<CocktailListItemMin> cocktails = new List<CocktailListItemMin>(RecentlyVisitedCocktails);
			CocktailListItemMin? cocktailInList = cocktails.Where(c => c.Cocktail_Id == cocktail.Cocktail_Id).SingleOrDefault();
			if (cocktailInList is not null)
			{
				cocktails.Remove(cocktailInList);
			}
			if (cocktails.Count == 5)
			{
				cocktails.Remove(cocktails[4]);
			}
			cocktails.Insert(0, cocktail);
			RecentlyVisitedCocktails = cocktails;
		}

		public void AddVisitedCocktail(Guid cocktail_id, string cocktail_name)
		{
			CocktailListItemMin cocktail = new CocktailListItemMin()
			{
				Cocktail_Id = cocktail_id,
				Name = cocktail_name
			};
			AddVisitedCocktail(cocktail);
		}
	}
}
