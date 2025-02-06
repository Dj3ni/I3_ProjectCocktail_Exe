using BLL.Entities;
using BLL.Mappers;
using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	//It has to be public because we need to communicate with frontend
	public class UserService : IUserRepository<User>
	{
		// To avoid confusion between DAL and BLL
		//private DAL.Services.UserService _service;
		//public UserService()
		//{
		//	_service = new DAL.Services.UserService();
		//	// Or _service = new D.UserService() if we use the using with alias.
		//}
		/* We can also use the dependency injection so everything is instanciated from ASP
		public UserService(DAL.Services.UserService userService)
		{
			_service = userService;
		}*/        /* If we used the repository pattern, we can use the Interface!*/
		private IUserRepository<DAL.Entities.User> _userService;
		private ICocktailRepository<DAL.Entities.Cocktail> _cocktailService;
		private ICommentRepository<DAL.Entities.Comment> _commentService;
		public UserService(
				IUserRepository<DAL.Entities.User> userService,
				ICocktailRepository<DAL.Entities.Cocktail> cocktailService)
		   {
				_userService = userService;
				_cocktailService = cocktailService;
		   }

		// 1. Crud

		public IEnumerable<User> GetAll()
		{
			// We need to convert Dal User to Bll User
			return _userService.GetAll().Select(dal => dal.ToBLL());
		}

		public User GetById(Guid id)
		{
			//return _userService.GetById(id).ToBLL();//we don't need select because we are not in a collection
			User user = _userService.GetById(id).ToBLL(); // We get the user
			user.Cocktails = _cocktailService.GetByUser(id).Select(dal =>dal.ToBLL()); // We get all the cocktails associated with him
			user.Comments = _commentService.GetByUserId(id).Select(dal => dal.ToBLL());

			return user;

		}

		public void Delete(Guid id)
		{
			_userService.Delete(id);
		}

		public Guid Insert(User user)
		{
			// we need a return to get the Id
			return _userService.Insert(user.ToDAL());
		}
		public void Update(Guid id, User user)
		{
			_userService.Update(id,user.ToDAL());
		}

		// 2. Connection (check Password)
		public Guid CheckPassword(string email, string password)
		{
			return _userService.CheckPassword(email, password);
		}
	}
}
