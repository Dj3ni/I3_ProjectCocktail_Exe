using ASP_MVC.Models.Auth;
using BLL.Entities;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{

	public class AuthController : Controller
	{
		private IUserRepository<BLL.Entities.User> _userService;

		public AuthController(IUserRepository<User> userService)
		{
			_userService = userService;
		}

		public IActionResult Index()
		{
			return RedirectToAction(nameof(Login));
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(AuthLoginForm loginForm)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException(nameof(loginForm));
				Guid id = _userService.CheckPassword(loginForm.Email, loginForm.Password);
				//Here we define session variable
				return RedirectToAction("Details","User", new {id});
			}
			catch (Exception)
			{
				return View();
			}
		}
	}
}
