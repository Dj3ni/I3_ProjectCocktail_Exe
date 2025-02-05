using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace ASP_MVC.Handlers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method)]
	public class ConnectionNeededAttribute : Attribute, IAuthorizationFilter
	{
		private string _action;
		private string _controller;
		private bool _getRouteValue;

		// constructeurs
		// Ici on dit que si rien n'est spécifié, par défaut on renvoie vers la page Login
		public ConnectionNeededAttribute() : this("Login", "Auth") { }

		//Ici on redirige vers une page précise
		public ConnectionNeededAttribute(string action, string controller, bool getRouteValue = false)
		{
			_action = action;
			_controller = controller;
			_getRouteValue = getRouteValue;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser)) is null)
			{
				object? routeValue = null;
				if (_getRouteValue)
				{
					routeValue = context.RouteData.Values;
				}
				// If not connected: we want the user to log in first before taking action
				context.Result = new RedirectToActionResult (_action,_controller,_getRouteValue);
			}
		}
	}
}
