namespace ASP_MVC.Handlers
{
	public class ConnectedUser
	{
		public Guid UserId { get; set; }
		public string UserName { get; set; }
		public string Email { get; set; }
		public DateTime ConnectedAt { get; set; }

	}
}
