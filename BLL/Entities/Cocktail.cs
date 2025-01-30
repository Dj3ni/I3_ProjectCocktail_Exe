using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
	public class Cocktail
	{
		public Guid Cocktail_Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string Instructions { get; set; }
		public DateTime CreatedAt { get; set; }
		public Guid? CreatedBy { get; set; }

		public Cocktail(Guid cocktail_Id, string name, string instructions, DateTime createdAt, string? description = null, Guid? createdBy = null)
		{
			Cocktail_Id = cocktail_Id;
			Name = name;
			Description = description;
			Instructions = instructions;
			CreatedAt = createdAt;
			CreatedBy = createdBy;
		}

		public Cocktail(string name, string instructions, string? description = null) : this(Guid.Empty,name, instructions,DateTime.Now,description, null)
		{
			Name = name;
			Description = description;
			Instructions = instructions;
		}


	}
}
