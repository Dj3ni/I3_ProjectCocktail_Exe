using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
	public class User
	{
		public Guid User_Id { get; set; }
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }

		private DateTime? _disabledAt;
		//public DateTime? DisabledAt { get { return _disabledAt; }}

		//Relation One To Many avec Cocktail
		public IEnumerable<Cocktail> Cocktails { get; set; }

		public bool IsDisabled
		{
			get { return _disabledAt is not null; }
		}

		public User(Guid user_Id, string first_Name, string last_Name, string email, string password, DateTime createdAt, DateTime? disabledAt)
		{
			User_Id = user_Id;
			First_Name = first_Name;
			Last_Name = last_Name;
			Email = email;
			Password = password;
			CreatedAt = createdAt;
			_disabledAt = disabledAt;
		}

		
			
	}
}
