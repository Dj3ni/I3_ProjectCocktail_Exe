using ASP_MVC.Mappers;
using ASP_MVC.Models.Cocktail;
using BLL.Entities;
using BLL.Services;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP_MVC.Controllers
{
	public class CocktailController : Controller
	{
		//Constructeur:
		private ICocktailRepository<BLL.Entities.Cocktail> _cocktailService;
		public CocktailController(ICocktailRepository<Cocktail> cocktailService)
		{
			_cocktailService = cocktailService;
		}

		// GET: CocktailController
		public ActionResult Index()
		{
			try
			{
				IEnumerable<Cocktail> model = (IEnumerable<Cocktail>)_cocktailService.GetAll().Select(bll => bll.ToListItem());
				return View(model);
			}
			catch (Exception)
			{

				return RedirectToAction("Error", "Home");
			}
		}

		// GET: CocktailController/Details/5
		public ActionResult Details(Guid id)
		{
			try
			{
				// On envoie le modèle de vue!
				CocktailDetails model = _cocktailService.GetCocktail(id).ToDetails();
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Error", "Home");
			}
		}

		// GET: CocktailController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CocktailController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CocktailCreate form)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
				// We need to catch the id and convert form data to BLL data
				Guid id = _cocktailService.Insert(form.ToBLL());

				return RedirectToAction(nameof(Details), new {id});
			}
			catch
			{
				return View();
			}
		}

		// GET: CocktailController/Edit/5
		public ActionResult Edit(Guid id)
		{
			CocktailEditForm model = _cocktailService.GetCocktail(id).EditForm();
			return View(model);
		}

		// POST: CocktailController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid id, CocktailEditForm form)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException();
				_cocktailService.Update(id, form.ToBLL());
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return RedirectToAction(nameof(Edit), new { id });
			}
		}

		// GET: CocktailController/Delete/5
		public ActionResult Delete(Guid id)
		{
			try
			{
				CocktailDelete model = _cocktailService.GetCocktail(id).DeleteForm();
				return View(model);
			}
			catch
			{
				return RedirectToAction("Error", "Home");
			}
		}

		// POST: CocktailController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, CocktailDelete form)
		{
			try
			{
				_cocktailService.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return RedirectToAction(nameof(Delete), new { id });
			}
		}
	}
}
