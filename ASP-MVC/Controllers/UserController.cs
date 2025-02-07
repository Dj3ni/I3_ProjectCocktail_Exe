using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Mappers;
using ASP_MVC.Models.User;
using BLL.Entities;
using BLL.Services;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{

	public class UserController : Controller
	{
		// with a repo pattern
		private IUserRepository<BLL.Entities.User> _userService;
		public UserController(IUserRepository<User> userService)
		{
			_userService = userService;
		}

		// We build a constructor for our controller to be able to inject Service in the actions.(if no repo pattern) 
		//private UserService _userService;
		//public UserController(UserService userService)
		//{
		//	_userService = userService;
		//}

		//If no dependency injection
		//public UserController()
		//{
		//	_userService = new UserService();
		//}

		// GET: UserController

		public ActionResult Index()
		{
			try
			{
				IEnumerable<UserListItem> model = _userService.GetAll().Select(bll => bll.ToListItem());
				return View(model);
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
			
		}

		// GET: UserController/Details/5
		public ActionResult Details(Guid id)
		{
			try
			{
				//We use the mapper method to convert BLL object to ASP object
				UserDetails model = _userService.GetById(id).ToDetails();
				
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Error", "Home");
			}
		}

		// GET: UserController/Create
		[AnonymousNeeded]
		public ActionResult Create()
		{
			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[AnonymousNeeded]
		public ActionResult Create(UserCreateForm form)
		{
			try
			{
				if (!form.Consent) ModelState.AddModelError(nameof(form.Consent), "Vous devez acceptez les termes et conditions pour continuer plus loin ");
				if (!ModelState.IsValid) throw new ArgumentException();
				//We need to convert the form into a BLL object
				Guid id = _userService.Insert(form.ToBLL());// we stock the id in the variable so we can use it for the redirect

				return RedirectToAction(nameof(Details), new {id});
			}
			catch
			{
				return View();
			}
		}

		// GET: UserController/Edit/5
		public ActionResult Edit(Guid id)
		{
			try
			{
				UserEditForm model = _userService.GetById(id).ToEditForm();//We need to use a mapper function to convert
				return View(model);
			}
			catch (Exception)
			{

				return RedirectToAction("Error", "home");
			}
		}

		// POST: UserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid id, UserEditForm form)
		{
			try
			{
				if(!ModelState.IsValid) throw new ArgumentException(nameof(form));
				_userService.Update(id, form.ToBLL());
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return RedirectToAction(nameof(Edit), new { id });
			}
		}

		// GET: UserController/Delete/5
		public ActionResult Delete(Guid id)
		{
			try
			{
				UserDelete model = _userService.GetById(id).ToDeleteForm();
				return View(model);
			}
			catch (Exception)
			{

				return RedirectToAction(nameof(Delete), new {id});
			}
		}

		// POST: UserController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, UserDelete form)
		{
			try
			{
				//no validation needed, it's not a true form
				_userService.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		//[AdminNeeded("Admin","Autor","User")]
		[AdminPermissionNeeded]
		public IActionResult ChangeRole(Guid id)
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		//[AdminNeeded("Admin", "Autor", "User")]
		[AdminPermissionNeeded]
		public IActionResult ChangeRole(Guid id, IFormCollection collection)
		{
			try
			{
				//Vérifier le formulaire
				//Demander un changement en DB
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				return View();
			}
		}
	}
}
