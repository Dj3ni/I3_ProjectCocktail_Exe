using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP_MVC.Handlers.ActionFilters
{
	[AttributeUsage(AttributeTargets.Method)] // Ne peut être utilisé que pour des méthodes de Controller!
	public class AnonymousNeededAttribute : Attribute, IActionFilter
	{
		// 
		public void OnActionExecuted(ActionExecutedContext context)
		{
			return; // Il ne doit rien faire!
		}

		// Contrôle avant d'exécuter l'action
		public void OnActionExecuting(ActionExecutingContext context)
		{
			if(context.HttpContext.Session.GetString(nameof(SessionManager.ConnectedUser)) is not null)
			{
				context.Result = new RedirectToActionResult("Index", "Home",null);
			}
		}
	}
}
