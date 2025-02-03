using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
	public interface ICocktailRepository<TCocktail> : ICRUDRepository<TCocktail,Guid>
	{
		//IEnumerable<TCocktail> GetAll();
		IEnumerable<TCocktail> GetByUser(Guid userId);
		//TCocktail GetCocktail(Guid cocktailId);

		//Guid Insert(TCocktail cocktail);
		//void Update(Guid id, TCocktail cocktail);
		//void Delete(Guid id);
	}
}
