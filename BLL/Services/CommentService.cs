using BLL.Entities;
using BLL.Mappers;
using Common.Repositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Comment = BLL.Entities.Comment;

namespace BLL.Services
{
	public class CommentService : ICommentRepository<Comment>
	{
		// 1. Get Differents services from de DAL to access data
		private IUserRepository<DAL.Entities.User> _userService;
		private ICocktailRepository<DAL.Entities.Cocktail> _cocktailService;
		private ICommentRepository<DAL.Entities.Comment> _commentService;

		public CommentService(
			IUserRepository<DAL.Entities.User> userService,
			ICocktailRepository<DAL.Entities.Cocktail> cocktailService,
			ICommentRepository<DAL.Entities.Comment> commentService
			)
		{
			_userService = userService;
			_cocktailService = cocktailService;
			_commentService = commentService;
		}

		// 2. Interface methods
		public IEnumerable<Comment> GetByCocktailId(Guid cocktailId)
		{
			IEnumerable<Comment> comments = _commentService.GetByCocktailId(cocktailId).Select(dal => dal.ToBLL());
			foreach (Comment comment in comments)
			{
				if (comment.CreatedBy is not null) comment.Creator = _userService.GetById((Guid)comment.CreatedBy).ToBLL();
			}
			return comments;
		}

		public IEnumerable<Comment> GetByUserId(Guid userId)
		{
			return _commentService.GetByUserId(userId).Select(dal => dal.ToBLL());
		}

		public Guid Insert(Comment comment)
		{
			return _commentService.Insert(comment.ToDAL());
		}

		public void Update(Guid commentId, Comment comment)
		{
			_commentService.Update(commentId, comment.ToDAL());
		}
		public void Delete(Guid commentId)
		{
			_commentService.Delete(commentId);
		}
	}
}
