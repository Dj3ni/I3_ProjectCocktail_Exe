using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class User
	{
		public Guid User_Id { get; set; }
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? DisabledAt { get; set; }
		public string Role { get; set; }
	}
}
