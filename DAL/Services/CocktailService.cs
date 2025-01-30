using Common.Repositories;
using DAL.Entities;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
	public class CocktailService : ICocktailRepository<Cocktail>
	{
		private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WAD24-DemoASP-DB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

		public IEnumerable<Cocktail> GetAll()
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Cocktail_GetAll";
					command.CommandType = CommandType.StoredProcedure;
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							yield return reader.ToCocktail();
						}
					}
				}
			}
		}

		public IEnumerable<Cocktail> GetByUser(Guid userId)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Cocktail_GetByUserId";
					command.CommandType= CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(User.User_Id), userId);
					connection.Open();
					using(SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							yield return reader.ToCocktail();
						}
					}
				}
			}

		}

		public Cocktail GetCocktail(Guid cocktailId)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Cocktail_GetById";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(Cocktail.Cocktail_Id), cocktailId);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return reader.ToCocktail();
						}
						else
						{
							throw new ArgumentOutOfRangeException();
						}
					}

				}
			}
		}

		public Guid Insert(Cocktail cocktail)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Cocktail_Inser";
					command.CommandType= CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(Cocktail.Name), cocktail.Name);
					command.Parameters.AddWithValue(nameof(Cocktail.Instructions), cocktail.Instructions);
					command.Parameters.AddWithValue(nameof(Cocktail.Description), cocktail.Description);
					command.Parameters.AddWithValue(nameof(Cocktail.CreatedBy),cocktail.CreatedBy);
					connection.Open();
					return (Guid)command.ExecuteScalar();
				}
			}
		}

		public void Update(Guid id, Cocktail cocktail)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Cocktail_Update";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(id), id);
					command.Parameters.AddWithValue(nameof(cocktail.Name), cocktail.Name);
					command.Parameters.AddWithValue(nameof(cocktail.Description), cocktail.Description);
					command.Parameters.AddWithValue(nameof(cocktail.Instructions),cocktail.Instructions);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
		public void Delete(Guid id)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Cocktail_Delete";
					command.CommandType= CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(Cocktail.Cocktail_Id), id);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
