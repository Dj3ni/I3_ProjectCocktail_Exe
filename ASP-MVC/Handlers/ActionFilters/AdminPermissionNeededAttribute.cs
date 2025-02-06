using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace ASP_MVC.Handlers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AdminPermissionNeededAttribute : Attribute, IAuthorizationFilter
	{
		private string[] _roles;

		public AdminPermissionNeededAttribute() : this("Admin") { }
		public AdminPermissionNeededAttribute(params string[] roles)
		{
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			string? json = context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser));
			if (json is null) { // utilisateur non connecté

				context.Result = new RedirectToActionResult("Home","Auth",null);
				return;
			}
			//Si connecté, on vérifie que dans les rôles autorisés
			ConnectedUser user = JsonSerializer.Deserialize<ConnectedUser>(json);
			if (!_roles.Contains(user.Role))
			{
				context.Result = new RedirectToActionResult("Index", "Home", null);
			}
		}
	}
}
