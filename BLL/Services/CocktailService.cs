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
		private ICocktailRepository<DAL.Entities.Cocktail> _service;
		public CocktailService(ICocktailRepository<DAL.Entities.Cocktail> service)
		{
			_service = service;
		}

		// Méthodes CRUD
		public IEnumerable<Cocktail> GetAll()
		{
			return _service.GetAll().Select(dal => dal.ToBLL());
		}

		public IEnumerable<Cocktail> GetByUser(Guid userId)
		{
			return _service.GetByUser(userId).Select(dal=>dal.ToBLL());
		}

		public Cocktail GetCocktail(Guid cocktailId)
		{
			return _service.GetCocktail(cocktailId).ToBLL();
		}

		public Guid Insert(Cocktail cocktail)
		{
			return _service.Insert(cocktail.ToDAL()); // convert in DAL object because we put in DB.
		}

		public void Update(Guid id, Cocktail cocktail)
		{
			_service.Update(id, cocktail.ToDAL());// no return because procedure ( void)
		}

		public void Delete(Guid id)
		{
			_service.Delete(id);
		}
	}
}
