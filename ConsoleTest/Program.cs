//using DAL.Entities;
//using DAL.Services;
using BLL.Entities;
using BLL.Services;
using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//	Console.WriteLine("Test User: Get all users");

			//	UserService service = new UserService();
			//	service.Delete(Guid.Parse("bbe5b03d-9a9f-43b5-90a2-6b07e1c0df3a"));

			//	//We use the GetFunction
			//	foreach (User user in service.GetAll())
			//	{
			//		if (user.IsDisabled)
			//		{
			//			Console.BackgroundColor = ConsoleColor.DarkRed;
			//			Console.ForegroundColor = ConsoleColor.White;
			//		}
			//		Console.WriteLine($"{user.User_Id} : {user.First_Name} {user.Last_Name} | {user.Email} | {user.Password}");
			//	}

			Console.WriteLine("Test Cocktails");

			//CocktailService service = new CocktailService();
			ServiceProvider serviceProvider = new ServiceCollection()
				.AddScoped<ICocktailRepository<DAL.Entities.Cocktail>, DAL.Services.CocktailService>()
				.AddScoped<BLL.Services.CocktailService>()
				.BuildServiceProvider();
			BLL.Services.CocktailService service = serviceProvider.GetRequiredService<BLL.Services.CocktailService>();

			foreach (Cocktail cocktail in service.GetAll()) {
				Console.WriteLine($"{cocktail.Cocktail_Id} : {cocktail.Name}");
			}
			Cocktail drink = service.GetById(Guid.Parse("2dd0250e-57d6-49bd-9a44-0d3a81de03fd"));

			Console.WriteLine($"{drink.Name}");

		}
	}
}
