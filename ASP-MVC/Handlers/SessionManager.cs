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
			//VisitedCocktails = new Queue<Cocktail>(); //pas utile, sauvegarde 2 *
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

		public Queue<Cocktail> VisitedCocktails {
			get
			{
				return ReadList();
			}
			private set
			{
				SaveList();
			}
		}		
		
		public void AddToVisited(Cocktail cocktail)
		{
			if( VisitedCocktails == null ) throw new ArgumentNullException(nameof(VisitedCocktails));

			//Correction: On crée direct une nouvelle liste qui reprend les infos de la session 
			Queue<Cocktail> cocktailList = new Queue<Cocktail>(VisitedCocktails);

			if(VisitedCocktails.Count()< _maxSizeList)
			{
				// Évite les doublons en supprimant l'existant
				if (VisitedCocktails.Any(c => c.Cocktail_Id == cocktail.Cocktail_Id))
				{
					VisitedCocktails = new Queue<Cocktail>(VisitedCocktails.Where(c => c.Cocktail_Id != cocktail.Cocktail_Id));
				}

				// Ajoute à la liste
				VisitedCocktails.Enqueue(cocktail);
			}
			else
			{
				VisitedCocktails.Dequeue();
				VisitedCocktails.Enqueue(cocktail);
			}
			SaveList();
		}


		public void SaveList()
		{
			_session.SetString(nameof(VisitedCocktails), JsonSerializer.Serialize(VisitedCocktails));
		}

		public Queue<Cocktail> ReadList()
		{
			string json = _session.GetString(nameof(VisitedCocktails));

			return string.IsNullOrEmpty(json) ? new Queue<Cocktail>() : JsonSerializer.Deserialize<Queue<Cocktail>>(json);

		}

	}
}
