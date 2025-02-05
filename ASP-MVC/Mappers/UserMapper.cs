using ASP_MVC.Models.User;

namespace ASP_MVC.Mappers
{
	internal static class UserMapper
	{
		public static UserListItem ToListItem(this BLL.Entities.User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			return new UserListItem()
			{
				User_Id = user.User_Id,
				First_Name = user.First_Name,
				Last_Name = user.Last_Name,
			};
		}

		public static UserDetails ToDetails(this BLL.Entities.User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			return new UserDetails()
			{
				User_Id = user.User_Id,
				First_Name = user.First_Name,
				Last_Name = user.Last_Name,
				Email = user.Email,
				CreatedAt = DateOnly.FromDateTime(user.CreatedAt),
				Cocktails = user.Cocktails.Select(bll=>bll.ToListItem()),
			};
		}

		// Convert createForm data to BLL data
		public static BLL.Entities.User ToBLL(this UserCreateForm user)
		{
			if(user == null) throw new ArgumentNullException( nameof(user));
			return new BLL.Entities.User(
					Guid.Empty, //we don't have it so we need to create one (that will be changed after )
					user.First_Name,
					user.Last_Name,
					user.Email,
					user.Password,
					DateTime.Now,
					null,
					"User"
				);
		}

		// Convert BLL data to editForm data 
		public static UserEditForm ToEditForm(this BLL.Entities.User user)
		{
			if( user == null) throw new ArgumentNullException( nameof(user));
			return new UserEditForm()
			{
				First_Name=user.First_Name,
				Last_Name=user.Last_Name,
				Email=user.Email,
			};
		}

		//Convert EditForm data to Bll
		public static BLL.Entities.User ToBLL(this UserEditForm user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			return new BLL.Entities.User(			
				Guid.Empty,
				user.First_Name,
				user.Last_Name,
				user.Email,
				"********",
				DateTime.Now,
				null,
				"User"
			);
		}

		// Convert BLL data to DeleteForm data 
		public static UserDelete ToDeleteForm(this BLL.Entities.User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));
			return new UserDelete()
			{
				First_Name = user.First_Name,
				Last_Name = user.Last_Name,
				Email = user.Email,
			};
		}
	}
}
