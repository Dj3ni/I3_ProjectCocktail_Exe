using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Repositories
{
	public	interface IUserRepository<TUser> : ICRUDRepository<TUser,Guid>
	{
		// We put all the methods from DAL and BLL Services.
		//IEnumerable<TUser> GetAll();
		//TUser Get(Guid id);

		//Guid Insert(TUser user);
		//void Update(Guid id, TUser user);
		//void Delete(Guid id);
		Guid CheckPassword(string email, string password);
	}
}
