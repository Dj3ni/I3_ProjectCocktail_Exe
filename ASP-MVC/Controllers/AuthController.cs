using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Models.Auth;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{

	public class AuthController : Controller
	{
		private IUserRepository<BLL.Entities.User> _userService;
		private SessionManager _sessionManager;

		public AuthController(IUserRepository<User> userService, SessionManager sessionManager)
		{
			_userService = userService;
			_sessionManager = sessionManager;
		}

		public IActionResult Index()
		{
			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		[AnonymousNeeded]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AnonymousNeeded]
		public IActionResult Login(AuthLoginForm loginForm)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException(nameof(loginForm));
				Guid id = _userService.CheckPassword(loginForm.Email, loginForm.Password);
				//Here we define session variable
				ConnectedUser user = new ConnectedUser()
				{
					UserId = id,
					Email = loginForm.Email,
					ConnectedAt = DateTime.Now,
				};
				_sessionManager.Login(user);
				return RedirectToAction("Details","User", new {id});
			}
			catch (Exception)
			{
				return View();
			}
		}

		public IActionResult Logout()
		{
			return View();
		}

		[HttpPost]
		
		public IActionResult Logout(IFormCollection form)
		{
			try
			{
				_sessionManager.Logout();
				return RedirectToAction(nameof(Login));
			}
			catch (Exception ex)
			{
				return View();
			}
		}
	}
}
