using Flashcards.Controllers;
using Flashcards.Helpers;
using Flashcards.NewFolder;
using Flashcards.Stores;
using Flashcards.Verification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Views
{
    internal class Navigation
    {
        public ListStore Store;
        public UIController UI;


        public Navigation()
        {

            Store = new ListStore();
            UI = new UIController();

        }

        public void MainMenu()
        {
            Console.Clear();

            UI.MainMenu();

            string input = Console.ReadLine();
            while (!Verifier.VerifyInput(input))
            {
                Console.WriteLine("Please enter valid input");
                input = Console.ReadLine();
            }
            switch (input)
            {
                case "1":
                    StudyNav study = new StudyNav();
                    study.CategorySelect();
                    break;
                case "2":
                    EditCategoriesMenu();
                    break;
                case "3":
                    ShowHistory();
                    break;
                case "0":
                    break;
                default:
                    break;
            }

        }

        public void ShowHistory()
        {
            Console.WriteLine("Enter year");
            Store.GetHistory(Console.ReadLine());
            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();
            MainMenu();
        }

        public void ShowCategories()
        {

            UI.DisplayCategories();
        }
        private void EditCategoriesMenu()
        {
            string header = "Edit Categories";
            string[] options = new[]
            {
                "1. Edit/Delete Existing Category",
                "2. Create New Category",
                "0, Return to Main Menu"
            };
            UI.DisplayInPanel(options, header);

            string selection = Console.ReadLine();
            while (!Verifier.VerifyInput(selection))
            {
                Console.WriteLine("Please enter valid input");
                selection = Console.ReadLine();
            }

            switch (selection)
            {
                case "1":
                    EditCategory();
                    break;
                case "2":
                    AddCategory();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid Selection");
                    EditCategoriesMenu();
                    break;
            }
        }

        private void EditCategory()
        {
            Console.Clear();
            string header = "Edit Categories";

            UI.DisplayInPanel(new[] { "Please select which category you would like to edit" }, header);
            ShowCategories();

            var input = Console.ReadLine();
            while (!Verifier.VerifyCategory(input, Store))
            {
                Console.WriteLine("Invalid selection. Please enter valid id or type 0 to return to main menu");

                input = Console.ReadLine();
                if (input == "0")
                {
                    MainMenu();
                }
            }
            Store.SetSelectedCategory(input);


            string[] options = new[]
            {
                "1. Edit Category Name",
                "2. Add/Delete Cards",
                "3. Delete Category",
                "0. Return to Main Menu"
            };

            UI.DisplayWithCategory(options, header);
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    EditCategoryName();
                    break;
                case "2":
                    AddEditQuestions();
                    break;
                case "3":
                    DeleteCategory();
                    break;
                case "0":
                    Console.Clear();
                    MainMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid selection");
                    Console.WriteLine();
                    EditCategory();
                    break;
            }
        }

        private void DeleteCategory()
        {
            Console.Clear();

            string header = "Delete Category";
            UI.DisplayWithCategory(new[] { "Are you sure you want to delete this category? This will delete all associated questions. Type DELETE to delete or NO to return to previous menu" }, header);

            string input = Console.ReadLine().ToLower();

            if (input == "delete")
            {
                Store.RemoveStack();
                Console.WriteLine("Category removed, press any key to return to main menu");
                Console.ReadKey();
                MainMenu();
            }
            else
            {
                EditCategory();
            }
        }

        private void EditCategoryName()
        {
            Console.Clear();

            UI.DisplayInPanel(new[] { "Please enter new category name" });
            string newName = Console.ReadLine();
            Store.UpdateStackName(newName);
            Console.Clear();
            UI.DisplayInPanel(new[] { "Name updated, press any key to return to main menu" });
            Console.ReadKey();
            Console.Clear();
            MainMenu();
        }
        private void AddCategory()
        {
            string header = "Add New Category";
            UI.DisplayInPanel(new[] { "Please enter a category name." }, header);

            string name = Console.ReadLine();

            Store.AddNewStack(name);

            UI.DisplayWithCategory(new[] { "Would you like to add cards to the new category? (Y/N)" }, header);

            string input = Console.ReadLine().ToLower();

            if (input == "y")
            {
                Store.SetSelectedCategory(name);

                AddQuestion();
            }
            else
            {
                MainMenu();
            }
        }

        private void AddEditQuestions()
        {
            Console.Clear();

            var options = new string[]
            {
                "1. Add New Cards",
                "2. Delete Existing Cards",
                "0. Return to Previous Menu"

            };
            UI.DisplayInPanel(options, "Add/Delete Cards");


            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AddQuestion();
                    break;
                case "2":
                    DeleteQuestion();
                    break;
                case "0":
                    MainMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Invalid Input");
                    Console.WriteLine();
                    AddEditQuestions();
                    break;
            }
        }

        private void AddQuestion()
        {
            Console.Clear();

            string header = "Add New Questions";
            UI.DisplayWithCategory(new[] { "Please input question" }, header);
            string question = Console.ReadLine();
            UI.DisplayWithCategory(new[] { "Please input answer" }, header);
            string answer = Console.ReadLine();
            Store.AddNewCard(question, answer);



            string[] content = new[]
            {

                "Would you like to add another? (Y/N)"
            };
            Console.Clear();
            UI.DisplayWithCategory(content, header);

            string input = Console.ReadLine().ToLower();

            while (!Verifier.VerifyInput(input))
            {
                Console.WriteLine("Enter valid input");
                input = Console.ReadLine().ToLower();
            }

            switch (input)
            {
                case "y":
                    AddQuestion();
                    break;
                case "n":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input, returned to main menu");
                    Console.WriteLine();
                    MainMenu();
                    break;
            }


        }
        private void DeleteQuestion()
        {
            Console.Clear();

            string header = "Delete Cards";
            UI.DisplayQuestions();
            UI.DisplayInPanel(new[] { "Please select a card to delete" }, header);

            var selection = Console.ReadLine();


            Store.SetSelectedCard(selection);
            int remaining = Store.RemoveCard();


            if (remaining == 0)
            {
                UI.DisplayInPanel(new[] { "No more cards to remove, press any key to return to main menu" }, header);
                Console.ReadKey();
                MainMenu();

            }
            else
            {
                string[] content = new[]
                {
                    $"There are now {remaining} cards left in {Store.CurrentSelected().StackName}",
                    "Would you like to delete another? (Y/N)"

                };

                UI.DisplayWithCategory(content, header);
                string input = Console.ReadLine().ToLower();
                if (input == "y")
                {

                    DeleteQuestion();
                }
                else
                {
                    MainMenu();
                }

            }
        }

    }
}
