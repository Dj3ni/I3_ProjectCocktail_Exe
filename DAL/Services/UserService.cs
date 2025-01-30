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
	public class UserService : IUserRepository<User>
	{
		private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WAD24-DemoASP-DB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

		//Searching the user in the list
		public IEnumerable<User> GetAll()
		{
			//Connection to DB
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				//Command
				using (SqlCommand command = connection.CreateCommand())
				{
					//Get with stocked procedure for the Sql command
					command.CommandText = "SP_User_GetAllActive";
					command.CommandType = System.Data.CommandType.StoredProcedure;
					connection.Open();
					// multiple infos so we use DataReader
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							//We use the mapper we created
							yield return reader.ToUser();
						}
					}
				}
			}
		}

		//Searching the User By the Id
		public User Get(Guid user_id)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_User_GetById";
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(user_id), user_id);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return reader.ToUser();
						}
						else
						{
							throw new ArgumentOutOfRangeException(nameof(user_id));
						}
					}

				}
			}
		}

		//Insert in DB
		public Guid Insert(User user) 
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_User_Insert";
					command.CommandType = CommandType.StoredProcedure;
					//Parameters
					command.Parameters.AddWithValue(nameof(User.First_Name), user.First_Name);
					command.Parameters.AddWithValue(nameof(User.Last_Name), user.Last_Name);
					command.Parameters.AddWithValue(nameof(User.Email), user.Email);
					command.Parameters.AddWithValue(nameof(User.Password), user.Password);

					connection.Open();
					return (Guid)command.ExecuteScalar(); 
				}
			}	
		}

		//Update
		public void Update(Guid user_id, User user)
		{
			using (SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_User_Update";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(user_id), user_id);
					command.Parameters.AddWithValue(nameof(User.Email), user.Email);
					command.Parameters.AddWithValue(nameof(User.First_Name), user.First_Name);
					command.Parameters.AddWithValue(nameof(User.Last_Name), user.Last_Name);

					connection.Open();
					command.ExecuteNonQuery(); //We don't wait for any result, we just want to execute the command
				}
			}
		}

		//Delete
		public void Delete(Guid user_id)
		{ 
			using(SqlConnection connection = new SqlConnection(ConnectionString))
			{
				using(SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_User_Delete";
					command.CommandType= CommandType.StoredProcedure;
					command.Parameters.AddWithValue(nameof(user_id),user_id);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}

		//Check Password
		public Guid CheckPassword(string email, string password)
		{
			using(SqlConnection conn = new SqlConnection(ConnectionString))
			{
				using(SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = "SP_User_CheckPassword";
					cmd.CommandType= CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue(nameof(email), email);
					cmd.Parameters.AddWithValue(nameof(password), password);
					conn.Open();
					return (Guid)cmd.ExecuteScalar();
				}
			}
		}
	}
}
