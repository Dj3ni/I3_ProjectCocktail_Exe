using Common.Repositories;

namespace ASP_MVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			/*Personalized Services if no repository pattern 
				//BLL
			builder.Services.AddScoped<BLL.Services.UserService>();
				//DAL
			builder.Services.AddScoped<DAL.Services.UserService>();*/

			//Personalized services
				//User
				builder.Services.AddScoped<IUserRepository<BLL.Entities.User>,BLL.Services.UserService>();
				builder.Services.AddScoped<IUserRepository<DAL.Entities.User>, DAL.Services.UserService>();
				//Cocktails
				
			builder.Services.AddScoped<ICocktailRepository<BLL.Entities.Cocktail>, BLL.Services.CocktailService>();
			builder.Services.AddScoped<ICocktailRepository<DAL.Entities.Cocktail>, DAL.Services.CocktailService>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
