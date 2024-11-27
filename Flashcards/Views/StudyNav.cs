using Flashcards.Controllers;
using Flashcards.Models;
using Flashcards.Stores;
using Flashcards.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Views
{
    internal class StudyNav
    {
        UIController Ui;
        ListStore Store;
        StudyController Session;

        public StudyNav()
        {
            Ui = new UIController();

            Store = new ListStore();
        }
        public void CategorySelect()
        {
            Console.Clear();
            Ui.DisplayInPanel(new[] {"Please select a category to study"}, "Category Select");
            Ui.DisplayCategories();
            string input = Console.ReadLine();
            while(!Verifier.VerifyCategory(input, Store))
            {
                Console.WriteLine("Please enter valid input");
                input = Console.ReadLine();
            }

            Store.SetSelectedCategory(input);
            Console.Clear();
            Ui.DisplayInPanel(new[] { "Press any key to begin" });
            Console.ReadKey();
            Session = new StudyController();

            Session.BeginStudy();
            
        }
    }
}
