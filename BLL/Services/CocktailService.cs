using BLL.Entities;
using BLL.Mappers;
using Common.Repositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cocktail = BLL.Entities.Cocktail;

namespace BLL.Services
{
	public class CocktailService : ICocktailRepository<Cocktail>
	{
		//Constructeur ( pour réutiliser un service si déjà créé)
		private ICocktailRepository<DAL.Entities.Cocktail> _cocktailService;
		private IUserRepository<DAL.Entities.User> _userService;
		public CocktailService(
			ICocktailRepository<DAL.Entities.Cocktail> cocktailService,
			IUserRepository<DAL.Entities.User> userRepository)
		{
			//We use both because relation between them
			_cocktailService = cocktailService;
			_userService = userRepository;
		}

		// Méthodes CRUD
		public IEnumerable<Cocktail> GetAll()
		{
			//return _cocktailService.GetAll().Select(dal => dal.ToBLL()); :: if no relations with table User

			// Here we want to get all the creators of all the cocktails
			IEnumerable<Cocktail> cocktails = _cocktailService.GetAll().Select(dal => dal.ToBLL());
			foreach (Cocktail cocktail in cocktails)
			{
				if (cocktail.Creator != null) cocktail.Creator = _userService.GetById((Guid)cocktail.CreatedBy).ToBLL();
			}
			return cocktails;
		}

		public IEnumerable<Cocktail> GetByUser(Guid userId)
		{
			return _cocktailService.GetByUser(userId).Select(dal=>dal.ToBLL());
		}

		public Cocktail GetById(Guid cocktailId)
		{
			//return _cocktailService.GetById(cocktailId).ToBLL(); //if no relations between cocktail and user
			// here we want to get the user Id
			Cocktail cocktail = _cocktailService.GetById(cocktailId).ToBLL();


			if (cocktail.CreatedBy is not null)
			{
				cocktail.Creator = _userService.GetById((Guid)cocktail.CreatedBy).ToBLL();
			}
			

			return cocktail;

		}

		public Guid Insert(Cocktail cocktail)
		{
			return _cocktailService.Insert(cocktail.ToDAL()); // convert in DAL object because we put in DB.
		}

		public void Update(Guid id, Cocktail cocktail)
		{
			_cocktailService.Update(id, cocktail.ToDAL());// no return because procedure ( void)
		}

		public void Delete(Guid id)
		{
			_cocktailService.Delete(id);
		}
	}
}
