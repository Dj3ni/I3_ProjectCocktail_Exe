using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ASP_MVC.Handlers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method)]
	public class IsCreatorAttribute : Attribute, IAuthorizationFilter
	{

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string? json = context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser));
			if (json is null)
			{

				context.Result = new RedirectToActionResult("Login", "Auth", null);
				return;
			}
			ConnectedUser user = JsonSerializer.Deserialize<ConnectedUser>(json);
			//Guid user_id = user.UserId;

			// On récupère l'id du cocktail via la route
			Guid cocktailId = Guid.Parse(context.RouteData.Values["id"].ToString());

			ICocktailRepository<Cocktail> cocktailRepository = GetCocktailService(context.HttpContext);
			Cocktail cocktail = cocktailRepository.GetById(cocktailId);
			if(cocktail.CreatedBy != user.UserId)
			{
				context.Result = new RedirectToActionResult("Details","Cocktail",new {id = cocktailId});
			}

		}


		// Permet de fournir l'injection de dépendance en-dehors du constructeur. en privé car plus safe

		private ICocktailRepository<Cocktail> GetCocktailService(HttpContext httpContext)
		{
			// Créer un service provider
			IServiceProvider serviceProvider = httpContext.RequestServices;
			//Ensuite aller chercher le service dont on a besoin
			 return serviceProvider.GetService<ICocktailRepository<Cocktail>>();
		}


	}
}
