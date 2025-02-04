using ASP_MVC.Handlers;
using ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP_MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly SessionManager _sessionManager;

		public HomeController(ILogger<HomeController> logger, SessionManager sessionManager)
		{
			_logger = logger;
			_sessionManager = sessionManager;
		}

		public IActionResult Index()
		{
			return View();
		}

		//Ne fonctionne que si on a configuré les Sessions!

		public IActionResult DemoSession()
		{
			// Sans HttpContextAccessor
			//int count = HttpContext.Session.GetInt32("CountVisitedPages") ?? 0;
			//count++;
			//HttpContext.Session.SetInt32("CountVisitedPage", count);

			//Avec HttpContextAccessor 
			int count = _sessionManager.CountVisitedPage;
			return View();
		}

		public IActionResult PlusOne()
		{
			// Sans HttpContextAccessor
			int count = HttpContext.Session.GetInt32("CountVisitedPages") ?? 0;
			count++;
			HttpContext.Session.SetInt32("CountVisitedPage", count);
			return View("DemoSession");
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
