using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//We can use the using with aliases to avoid confusion between objects with the same name
// using BLL.Entities;
//using D = DAL.Entities;

namespace BLL.Mappers
{
	internal static class Mapper
	{
		// USER: We will convert the DAL object to BLL object
		public static BLL.Entities.User ToBLL(this DAL.Entities.User user)
		{
			if(user == null) throw new ArgumentNullException(nameof(user));
			return new BLL.Entities.User(
					user.User_Id,
					user.First_Name,
					user.Last_Name,
					user.Email,
					user.Password,
					user.CreatedAt,
					user.DisabledAt);
		}

		//USER: Function to convert object BLL to DAL Object
		public static DAL.Entities.User ToDAL(this BLL.Entities.User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			return new DAL.Entities.User()
			{
				User_Id = user.User_Id,
				First_Name = user.First_Name,
				Last_Name = user.Last_Name,
				Password = user.Password,
				Email = user.Email,
				CreatedAt = user.CreatedAt,
				// Disabled at is private we cannot change it, so we use the bool IsDisabled
				DisabledAt = (user.IsDisabled) ? null: new DateTime()
			};
		}


		//COCKTAIL: From DAL data to Bll data
		public static BLL.Entities.Cocktail ToBLL(this DAL.Entities.Cocktail cocktail)
		{
			if (cocktail == null) throw new ArgumentNullException(nameof(cocktail));
			// we use constructor
			return new BLL.Entities.Cocktail(
				cocktail.Cocktail_Id,
				cocktail.Name,
				cocktail.Instructions,
				(DateTime)cocktail.CreatedAt,
				(cocktail.Description is null) ? null : cocktail.Description,
				(cocktail.CreatedBy is null) ? null : (Guid)cocktail.CreatedBy
			);
		}

		//COCKTAIL: From BLL data to DAL data
		public static DAL.Entities.Cocktail ToDAL(this BLL.Entities.Cocktail cocktail)
		{
			if (cocktail == null) throw new ArgumentNullException(nameof(cocktail));
			//we don't have constructor so we assign to properties
			return new DAL.Entities.Cocktail()
			{
				Cocktail_Id = cocktail.Cocktail_Id,
				Name = cocktail.Name,
				Instructions = cocktail.Instructions,
				Description = cocktail.Description,
				CreatedAt = cocktail.CreatedAt,
				CreatedBy = cocktail.CreatedBy,
			};
		}
	}
}
