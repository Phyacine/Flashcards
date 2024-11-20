
using Flashcards.DatabaseActions;
using Flashcards.NewFolder;
using Flashcards.UserInterface;
using Microsoft.Data.SqlClient;


namespace Flashcards
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-1758C90;Database=Flashcards;User=user;Password=password;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
            List<StackItem> Stacks = new List<StackItem>();
            try
            {
                connection.Open();
                Stacks = LoadStacks.Load(connectionString);
                foreach(StackItem a in Stacks)
                {
                    Console.WriteLine(a.StackId + " " + a.StackName);

                    foreach(FlashCardDto b in a.FlashCards)
                    {
                        Console.WriteLine(b.Question);
                    }
                }
                Console.WriteLine("Connected");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            MainMenu.MenuOptions(connectionString);
            Console.ReadKey();

        }
    }
}
