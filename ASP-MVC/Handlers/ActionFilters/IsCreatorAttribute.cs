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

				context.Result = new RedirectToActionResult("Home", "Auth", null);
				return;
			}
			ConnectedUser user = JsonSerializer.Deserialize<ConnectedUser>(json);
			Guid user_id = user.UserId;
		}


		// Permet de fournir l'injection de dépendance en-dehors du constructeur

	}
}
