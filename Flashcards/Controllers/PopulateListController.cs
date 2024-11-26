using Microsoft.IdentityModel.Tokens;
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
    internal class PopulateListController
    {
        private readonly string ConnectionString;
        public PopulateListController(string connectionString)
        {
            ConnectionString = connectionString;

        }

        public void PopulateAll(List<FlashCard> cards, List<FlashCardDto> dtos, List<StackItem> stacks)
        {
            PopulateCard(cards);
            PopulateStack(stacks);
            PopulateStacks(stacks, cards);

        }

        public void PopulateCard(List<FlashCard> cards)
        {
            cards.Clear();
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    var sql = "SELECT * FROM Cards";
                    var command = new SqlCommand(sql, connection);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new FlashCard
                        {
                            FlashCardId = (int)reader["CardId"],
                            Question = (string)reader["Question"],
                            Answer = (string)reader["Answer"],
                            StackId = (int)reader["StackId"]

                        });
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public void PopulateStack(List<StackItem> stacks)
        {
            stacks.Clear();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Stacks";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stacks.Add(new StackItem
                            {
                                StackId = (int)reader["StackId"],
                                StackName = (string)reader["Category"]
                            });

                        }
                    }
                }

            }
        }
        public void PopulateStacks(List<StackItem> stacks, List<FlashCard> cards)
        {
            foreach (StackItem x in stacks)
            {
                foreach (FlashCard y in cards)
                {
                    if (x.StackId == y.StackId)
                    {
                        x.FlashCards.Add(new FlashCardDto
                        {
                            FlashCardId = y.FlashCardId,
                            Question = y.Question,
                            Answer = y.Answer,
                            DtoId = x.FlashCards.Count() + 1

                        });

                    }
                }

            }
        }
        private void Execute(string query)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand(query, connection);
                    var reader = command.ExecuteReader();
                }
                catch { }
            }
        }
    }
}
