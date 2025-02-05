using BLL.Entities;
using System.Text.Json;

namespace ASP_MVC.Handlers
{
	public class SessionManager
	{
		private readonly ISession _session;

		//Dans le constructeur on va chercher l'accesseur de service pour activer la Session
		public SessionManager(IHttpContextAccessor accessor)
		{
			_session = accessor.HttpContext.Session;
		}

		// Example
		public int CountVisitedPage
		{
			get { return _session.GetInt32(nameof(CountVisitedPage)) ?? 0; }
			set { _session.SetInt32(nameof(CountVisitedPage), value); }
		}

		/****** Savoir si User connecté *********/
		public ConnectedUser? ConnectedUser
		{
			get { return JsonSerializer.Deserialize<ConnectedUser?>( _session.GetString(nameof(ConnectedUser))?? "null"); }
			set {
				if (value is null)
				{
					_session.Remove(nameof(ConnectedUser));
				}
				else
				{
					_session.SetString(nameof(ConnectedUser),JsonSerializer.Serialize(value));
				}

			}
		}

		public void Login(ConnectedUser user)
		{
			ConnectedUser = user;
		}

		public void Logout()
		{
			ConnectedUser = null;
		}

		/**** Exe Liste des 5 derniers cocktails consultés ******/
		private const short  _maxSizeList = 5;

		public Queue<Cocktail> VisitedCocktails { get; private set; }
		//{
		//	get { return new List<Cocktail>().ToArray(); }
		//	private set;
		//}
		
		public void AddToVisited(Cocktail cocktail)
		{
			if( VisitedCocktails == null ) throw new ArgumentNullException(nameof( VisitedCocktails));
			if(VisitedCocktails.Count()< _maxSizeList)
			{
				//VisitedCocktails.ToList().Add(cocktail);
				VisitedCocktails.Enqueue(cocktail);
			}
			else
			{
				VisitedCocktails.Dequeue();
				VisitedCocktails.Enqueue(cocktail);
			}
		}

	}
}
