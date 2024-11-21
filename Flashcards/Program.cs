
using Flashcards.DatabaseActions;
using Flashcards.Items;
using Flashcards.NewFolder;
using Flashcards.UserInterface;
using Microsoft.Data.SqlClient;
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
            foreach(StackItem a in ListStore.Stacks)
            {
                Console.WriteLine(a.StackName);
                foreach(FlashCardDto b in a.FlashCards)
                {
                    Console.WriteLine(b.Question);
                }
            }

            Console.ReadKey();

        }
    }
}
