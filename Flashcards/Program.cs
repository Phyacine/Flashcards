
using Flashcards.Controllers;
using Flashcards.Helpers;
using Flashcards.NewFolder;
using Flashcards.Stores;
using Flashcards.Views;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;


namespace Flashcards
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            

            try
            {
                CreateTables createTables = new CreateTables();
                ListStore listStore = new ListStore();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                
            }

            Navigation nav = new Navigation();
            nav.MainMenu();
            Console.ReadKey();

        }
    }
}
