using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
	public interface ICommentRepository<TComment>
	{
		IEnumerable<TComment> GetByCocktailId(Guid cocktailId);
		IEnumerable<TComment> GetByUserId(Guid userId);
		Guid Insert(TComment comment);
		void Delete(Guid commentId);
		void Update(Guid commentId, TComment comment);
	}
}
