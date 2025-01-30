using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
	//Only class that can be internal (because no use with other projects
	internal static class Mapper
	{
		public static User ToUser(this IDataRecord record)
		{
			if (record is null) throw new ArgumentNullException(nameof(record)); //Or if(record is null) return null;
			return new User()
			{
				User_Id = (Guid)record[nameof(User.User_Id)],
				First_Name = (string)record[nameof(User.First_Name)],
				Last_Name = (string)record[nameof(User.Last_Name)],
				Email = (string)record[nameof(
									User.Email)],
				Password = "********",
				DisabledAt = (record[nameof(User.DisabledAt)] is DBNull) ? null : (DateTime?)record[nameof(User.DisabledAt)],
			};
		}

		public static Cocktail ToCocktail(this IDataRecord record)
		{
			if (record is null) throw new ArgumentNullException(nameof(record)); //Or if(record is null) return null;

			return new Cocktail()
			{
				Cocktail_Id = (Guid)record[nameof(Cocktail.Cocktail_Id)],
				Name = (string)record[nameof(Cocktail.Name)],
				Instructions = (string)record[nameof(Cocktail.Instructions)],
				Description = (record[nameof(Cocktail.Description)] is DBNull) ? null : (string?) record[nameof(Cocktail.Description)],
				CreatedAt = (DateTime)record[nameof(Cocktail.CreatedAt)],
				CreatedBy = (record[nameof(Cocktail.CreatedBy)] is null) ? null : (Guid?)record[nameof(Cocktail.CreatedBy)]
			};
		}

	}
}
