using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;
using ASP_MVC.Mappers;
using ASP_MVC.Models.Cocktail;
using AspNetCoreGeneratedDocument;
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
		private readonly SessionManager _sessionManager;
		private ICommentRepository<Comment> _commentService;
		//private readonly CocktailQueueCorrection _cocktailQueueCorrection;

		public CocktailController(
			ICocktailRepository<Cocktail> cocktailService,
			SessionManager sessionManager,
			ICommentRepository<Comment> commentService
			//CocktailQueueCorrection cocktailQueue
			)
		{
			_cocktailService = cocktailService;
			_sessionManager = sessionManager;
			_commentService = commentService;
			//_cocktailQueueCorrection = cocktailQueue;
		}

		// GET: CocktailController
		public ActionResult Index()
		{
			try
			{
				//attention qu'on a besoin d'un listItem et non d'un cocktail!
				IEnumerable<CocktailListItem> model = _cocktailService.GetAll().Select(bll => bll.ToListItem());
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
				CocktailDetails model = _cocktailService.GetById(id).ToDetails();
				_sessionManager.AddVisitedCocktail(model.Cocktail_Id, model.Cocktail_Name);
				model.Comments = _commentService.GetByCocktailId(id).Select(bll => bll.ToListItem());
				//_cocktailQueueCorrection.AddVisitedCocktail(model.Cocktail_Id, model.Cocktail_Name);

				//_sessionManager.AddToVisited(model.Cocktail_Id);
				//ViewData["VisitedCocktails"] = _sessionManager.VisitedCocktails.Select(c => c.ToListItem()).ToList();
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Error", "Home");
			}
		}

		[ConnectionNeeded]
		// GET: CocktailController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CocktailController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ConnectionNeeded]
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
		//[ConnectionNeeded("Details", "Cocktail", true)]
		[IsCreator]
		public ActionResult Edit(Guid id)
		{
			try
			{
				Cocktail cocktail = _cocktailService.GetById(id);
				/* if we don't use the isCreator attribute, we would have to do this verification for each method we want to restrain acces to
				 * 
				if(!(_sessionManager.ConnectedUser.UserId == cocktail.CreatedBy)) throw new InvalidOperationException("Vous n'êtes pas l'auteur de ce cocktail!");*/
				CocktailEditForm model = cocktail.EditForm();
				return View(model);
			}
			catch(Exception ex)
			{
				TempData["ErrorMessage"] = ex.Message;
				return RedirectToAction(nameof(Index));
			}
			
		}

		// POST: CocktailController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		//[ConnectionNeeded]
		[IsCreator]
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
		//[ConnectionNeeded]
		[IsCreator]
		public ActionResult Delete(Guid id)
		{
			try
			{
				CocktailDelete model = _cocktailService.GetById(id).DeleteForm();
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
		//[ConnectionNeeded]
		[IsCreator]
		
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
