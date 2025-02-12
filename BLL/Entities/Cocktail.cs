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

		//Relation Many to One avec User: 
		public User? Creator { get; set; } // pour faire le lien avec la table User

		public List<Comment> Comments { get; set; }

		public Cocktail(Guid cocktail_Id, string name, string instructions, DateTime createdAt, string? description = null, Guid? createdBy = null)
		{
			Cocktail_Id = cocktail_Id;
			Name = name;
			Description = description;
			Instructions = instructions;
			CreatedAt = createdAt;
			CreatedBy = createdBy;
		}

		public Cocktail(Guid cocktail_Id, string name, string instructions, string? description = null)
		{
			Cocktail_Id = cocktail_Id;
			Name = name;
			Description = description;
			Instructions = instructions;
		}


		public void AddComment(Comment comment)
		{
			Comments.Add(comment);
		}

		public void SetComments(IEnumerable<Comment> comments)
		{
			Comments = new List<Comment>(comments);
		}

	}
}
