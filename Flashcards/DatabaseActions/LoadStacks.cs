using Flashcards.NewFolder;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.DatabaseActions
{
    internal static class LoadStacks
    {
        public static List<StackItem> Stacks = new List<StackItem>();

        public static void CreateTables(string connectionString)
        {
            var stacksQuery = "CREATE TABLE Stacks(" +
                "Category nvarchar(50) NOT NULL," +
                "StackId int NOT NULL AUTO_INCREMENT" +
                "PRIMARY KEY(StackId)";
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(stacksQuery, connection);
                //command.
            }


        }
        public static List<StackItem> Load(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Stacks";
                var command = new SqlCommand(sql, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Stacks.Add(new StackItem
                    {
                        StackId = (int)reader["StackId"],
                        StackName = (string)reader["StackName"]
                    });

                    
                }
                reader.Close();
                foreach(StackItem stack in Stacks)
                {
                    var sql2 = $"SELECT * FROM Flashcards WHERE StackId={stack.StackId}";
                    var command2 = new SqlCommand(sql2, connection);
                    var reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        stack.FlashCards.Add(new FlashCardDto
                        {
                            FlashCardId = (int)reader2["FlashCardId"],
                            Question = (string)reader2["Question"],
                            Answer = (string)reader2["Answer"]
                        });
                    }
                    reader2.Close();
                    
                }

                connection.Close();
            }
            return Stacks;
        }
        public static void AddStack(string connectionString)
        {
            string newStackName = "";
            

            while (newStackName == "")
            {
                Console.WriteLine("Please enter a name for the category.");
                newStackName = Console.ReadLine();

            }

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var sql = "INSERT INTO Stacks (StackName) VALUES (@StackName)";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StackName", newStackName);
                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
        }
    }
}
