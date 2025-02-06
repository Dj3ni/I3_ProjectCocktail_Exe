using Common.Repositories;
using DAL.Entities;
using DAL.Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
	public class CommentService : BaseService, ICommentRepository<Comment>
	{
		// On accède au service pour gérer la string connection
		public CommentService(IConfiguration configuration) : base(configuration, "Main-DB") { }

		// Fonctions du ICommentRepository
		
		public IEnumerable<Comment> GetByCocktailId(Guid cocktailId)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_GetByCocktailId";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("cocktail_id", cocktailId);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							//We need to convert SqlData to Dal object
							yield return reader.ToComment();
						}
					}
				}
			}
		}

		public IEnumerable<Comment> GetByUserId(Guid userId)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_GetByUserId";
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("user_id", userId);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							//We need to convert SqlData to Dal object
							yield return reader.ToComment();
						}
					}
				}
			}
		}

		public Guid Insert(Comment comment)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_Insert";
					command.CommandType = CommandType.StoredProcedure;
					//Parameters
					command.Parameters.AddWithValue("user_id", (object?)comment.CreatedBy ?? DBNull.Value);//the parameter name is different from the column so we need to take the one from the SP
					command.Parameters.AddWithValue("cocktail_id", comment.Concern);
					command.Parameters.AddWithValue(nameof(Comment.Title), comment.Title);// same name between parameter and column so we can use nameof
					command.Parameters.AddWithValue(nameof(Comment.Content), comment.Content);
					command.Parameters.AddWithValue(nameof(Comment.Note), (object?)comment.Note ?? DBNull.Value); // null c# and null sql are different so we need to check
					//Proceed and get the Id
					connection.Open();
					return(Guid)command.ExecuteScalar();

				}
			}
		}

		public void Update(Guid commentId, Comment comment)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_Update";
					command.CommandType = CommandType.StoredProcedure;
					//Parameters
					command.Parameters.AddWithValue(nameof(Comment.Comment_Id), commentId);
					command.Parameters.AddWithValue(nameof(Comment.Title), comment.Title);
					command.Parameters.AddWithValue(nameof(Comment.Content), comment.Content);
					command.Parameters.AddWithValue(nameof(Comment.Note), comment.Note);
					//Proceed
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
		public void Delete(Guid commentId)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandText = "SP_Comment_Delete";
					command.CommandType = CommandType.StoredProcedure;
					//Parameters
					command.Parameters.AddWithValue(nameof(Comment.Comment_Id), commentId);

					//Proceed
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
