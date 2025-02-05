using Common.Repositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeDAL.Services
{
	public class UserService : IUserRepository<DAL.Entities.User>
	{
		private static List<User> _users = new List<User>() {
			new User (){
				User_Id = Guid.NewGuid(),
				First_Name = "Samuel",
				Last_Name = "Legrain",
				Email = "samuel.legrain@bstorm.be",
				CreatedAt = DateTime.Now,
				DisabledAt = null,
				Password = "********"},
			new User (){
				User_Id = Guid.NewGuid(),
				First_Name = "Michaël",
				Last_Name = "Person",
				Email = "michael.person@bstorm.be",
				CreatedAt = DateTime.Now,
				DisabledAt = null,
				Password = "********"},
			new User (){
				User_Id = Guid.NewGuid(),
				First_Name = "Thierry",
				Last_Name = "Morre",
				Email = "thierry.morre@bstorm.be",
				CreatedAt = DateTime.Now,
				DisabledAt = null,
				Password = "********"},
		};

		public Guid CheckPassword(string email, string password)
		{
			throw new NotImplementedException();
		}

		public void Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<User> GetAll()
		{
			return _users;
		}

		public User Get(Guid id)
		{
			return _users.Where(u => u.User_Id == id).SingleOrDefault();
		}

		public Guid Insert(User user)
		{
			throw new NotImplementedException();
		}

		public void Update(Guid id, User user)
		{
			throw new NotImplementedException();
		}
	}
}
