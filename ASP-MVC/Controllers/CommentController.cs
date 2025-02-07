using ASP_MVC.Models.Comment;
using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP_MVC.Mappers;
using BLL.Entities;
using ASP_MVC.Handlers;
using ASP_MVC.Handlers.ActionFilters;

namespace ASP_MVC.Controllers
{
	public class CommentController : Controller
	{
		// 1. constructeur pour accéder au service de Comment

		private ICommentRepository<Comment> _commentService;
		private readonly SessionManager _sessionManager;
		public CommentController( 
			ICommentRepository<Comment> commentService,
			SessionManager sessionManager
			)
		{
			_commentService = commentService;
			_sessionManager = sessionManager;
		}



		// GET: CommentController
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Home");

			//return RedirectToAction(nameof(UserComments), new { id = _sessionManager.ConnectedUser.UserId });
		}

		//[IsCreator]
		//// GET: CommentController/Details/5
		//public ActionResult Details(Guid id)
		//{
		//	CommentDetails model = _commentService.GetByUserId(_sessionManager.ConnectedUser.UserId)
		//											.FirstOrDefault(comment => comment.Comment_Id == id)
		//											.ToDetails();
		//	return View(model);
		//}

		//[IsCreator]
		//public ActionResult UserComments(Guid userId)
		//{
		//	IEnumerable<CommentListItem> model = _commentService.GetByUserId(userId).Select(bll => bll.ToListItem());
		//	return View(model);
		//}
		//[ConnectionNeeded]
		//public ActionResult CocktailComments(Guid cocktailId)
		//{
		//	IEnumerable<CommentListItem> model = _commentService.GetByCocktailId(cocktailId).Select(bll => bll.ToListItem());
		//	return View(model);
		//}

		// GET: CommentController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CommentController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CommentCreateForm form)
		{
			try
			{
				if (!ModelState.IsValid) throw new ArgumentException(nameof(form));
				Guid id = _commentService.Insert(form.ToBLL());
				return RedirectToAction("Index","Cocktail");
			}
			catch
			{
				return View();
			}
		}

		// GET: CommentController/Edit/5
		public ActionResult Edit(Guid id)
		{
			try
			{
				CommentEditForm model = _commentService.GetByUserId(_sessionManager.ConnectedUser.UserId)
															.FirstOrDefault(comment => comment.Comment_Id == id)
															.ToEditForm();
				return View(model);
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Home");
			}
		}

		// POST: CommentController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Guid id, CommentEditForm form)
		{
			try
			{
				if(!ModelState.IsValid) throw new ArgumentException(nameof(form));
				_commentService.Update(id,form.ToBLL());

				return RedirectToAction("Index", "Home");
			}
			catch
			{
				return View();
			}
		}

		// GET: CommentController/Delete/5
		public ActionResult Delete(Guid id)
		{
			try
			{
				CommentDelete model = _commentService.GetByUserId(_sessionManager.ConnectedUser.UserId)
															.FirstOrDefault(comment => comment.Comment_Id == id)
															.ToDeleteForm();
				return View(model);
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}
		}

		// POST: CommentController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, CommentDelete form)
		{
			try
			{
				if(!ModelState.IsValid) throw new ArgumentException(nameof(_commentService));
				_commentService.Delete(id);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
