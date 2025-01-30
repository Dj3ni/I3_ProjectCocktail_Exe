using Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Cocktail 
	{
		public Guid Cocktail_Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Instructions { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid? CreatedBy { get; set; }
	}
}
