using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
	// Ici on crée l'interface de toute entité qui aura besoin d'un crud
	public interface ICRUDRepository<TEntity,TId>
	{
		IEnumerable<TEntity> GetAll();
		TEntity GetById(TId id);
		TId Insert(TEntity entity);
		void Update(TId id, TEntity entity);
		void Delete(TId id);
	}
}
