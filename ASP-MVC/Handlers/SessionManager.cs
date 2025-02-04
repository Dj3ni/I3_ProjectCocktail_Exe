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

		//Savoir si User connecté
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

	}
}
