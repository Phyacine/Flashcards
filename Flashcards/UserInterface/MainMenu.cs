using Flashcards.DatabaseActions;
using Flashcards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.UserInterface
{
    internal static class MainMenu
    {
        
        public static void MenuOptions(string connectionString, ListStore store)
        {
            Console.WriteLine("Welcome! Please select an option");

            Console.WriteLine("1. Start studying");
            Console.WriteLine("2. Edit study topics");
            Console.WriteLine("3. Show study history");
            Console.WriteLine("0. Close program.");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    
                    break;
                case "2":

                    break;
                case "3":
                    break;
                case "0":
                    break;
                default:
                    break;
            }

        }
        public static void ShowCategories(ListStore store)
        {
            
        }

        public static void CreateStack()
        {
            
        }
    }
}
