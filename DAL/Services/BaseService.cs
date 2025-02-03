using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
	public abstract class BaseService
	{
		protected readonly string _connectionString;

		//public BaseService(IConfiguration config)
		//{
		//	_connectionString = config.GetConnectionString("Main-DB") ?? throw new Exception("Pas de connectionString correspondante");
		//}

		//Pour le rendre réutilisable, peu importe la connection string, on doit ajouter un deuxième paramètre. Il faudra donc préciser dans le constructeur de chaque enfant le nom de la DB
		public BaseService(IConfiguration config, string dbName)
		{
			_connectionString = config.GetConnectionString(dbName) ?? throw new Exception("Pas de connectionString correspondante");
		}
	}
}
