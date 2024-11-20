
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
            LoadStacks loadDb = new LoadStacks();
            string connectionString = "Server=DESKTOP-1758C90;Database=Flashcards;User=user;Password=password;TrustServerCertificate=True";
            SqlConnection connection = new SqlConnection(connectionString);
            List<StackItem> Stacks = new List<StackItem>();
            try
            {
                connection.Open();
                loadDb.CreateStacks(connectionString);
                loadDb.CreateCards(connectionString);

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
