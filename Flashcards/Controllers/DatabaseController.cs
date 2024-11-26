using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.NewFolder;
using Flashcards.Models;

namespace Flashcards.Controllers
{
    internal class DatabaseController
    {
        public void UpdateStackItem(StackItem stack)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "UPDATE Stacks SET Category = @Category " +
                        "WHERE StackId = @StackId;";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Category", stack.StackName);
                        command.Parameters.AddWithValue("@StackId", stack.StackId);
                        command.ExecuteNonQuery();
                    };

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public void DeleteStack(StackItem stack)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Stacks WHERE StackId = @StackId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@StackId", stack.StackId);
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }
        public void AddNewStackItem(StackItem stack)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Stacks (Category) VALUES (@Category)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Category", stack.StackName);
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        public StackItem GetStackByCategory(string category)
        {
            StackItem stack = new StackItem();
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "SELECT * FROM Stacks WHERE Category = @Category";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            stack.StackId = (int)reader["StackId"];
                            stack.StackName = (string)reader["Category"];

                        }
                    }
                }
                catch (Exception ex) { }
            }
            return stack;
        }
        public void AddFlashCard(string question, string answer, StackItem stack)
        {

            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "INSERT INTO Cards (Question, Answer, StackId) VALUES (@Question, @Answer, @StackId)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Question", question);
                        command.Parameters.AddWithValue("@Answer", answer);
                        command.Parameters.AddWithValue("@StackId", stack.StackId);
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }
        public void RemoveFlashCard(FlashCardDto flashcard)
        {
            using (var connection = new SqlConnection(Constants.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string sql = "DELETE FROM Cards WHERE CardId = @FlashCardId";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FlashCardId", flashcard.FlashCardId);
                        command.ExecuteNonQuery();
                    }
                }
                catch { }
            }
        }
    }
}
