using ASP_MVC.Handlers;
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

			// Add Session services 

			//Ce service n'est utilisé que pour le developpement et debogage 
			//builder.Services.AddDistributedMemoryCache();

			// Pour la prod on va plutôt utiliser ceci:
			builder.Services.AddDistributedSqlServerCache(
				options =>{
					options.ConnectionString = builder.Configuration.GetConnectionString("Session-DB");
					options.SchemaName = "dbo";
					options.TableName = "Session";
				});

			builder.Services.AddSession(
				options =>
				{
					options.Cookie.Name = "CookieWad24";
					options.Cookie.HttpOnly = true;
					options.Cookie.IsEssential = true;
					options.IdleTimeout = TimeSpan.FromMinutes(10);
				}
			);
			builder.Services.Configure<CookiePolicyOptions>(
				options =>
				{
					options.CheckConsentNeeded = context => true;
					options.MinimumSameSitePolicy = SameSiteMode.None; // même règle de police pour tous les cookies
					options.Secure = CookieSecurePolicy.Always;
				}
			);

			// Add HttpContext service
			builder.Services.AddHttpContextAccessor();


			/*Personalized Services if no repository pattern 
				//BLL
			builder.Services.AddScoped<BLL.Services.UserService>();
				//DAL
			builder.Services.AddScoped<DAL.Services.UserService>();*/

			//Personalized services
				// USER
				builder.Services.AddScoped<IUserRepository<BLL.Entities.User>,BLL.Services.UserService>();
				builder.Services.AddScoped<IUserRepository<DAL.Entities.User>, DAL.Services.UserService>();
				// COCKTAILS				
				builder.Services.AddScoped<ICocktailRepository<BLL.Entities.Cocktail>, BLL.Services.CocktailService>();
				builder.Services.AddScoped<ICocktailRepository<DAL.Entities.Cocktail>, DAL.Services.CocktailService>();
				// COMMENTS
				builder.Services.AddScoped<ICommentRepository<BLL.Entities.Comment>, BLL.Services.CommentService>();
				builder.Services.AddScoped<ICommentRepository<DAL.Entities.Comment>, DAL.Services.CommentService>();

			//Service SessionManager
			builder.Services.AddScoped<SessionManager>();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseSession();
			app.UseCookiePolicy();

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
