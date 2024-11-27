using Flashcards.Models;
using Flashcards.NewFolder;
using Flashcards.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Controllers
{
    internal class StudyController
    {
        StackItem Category;
        StudySession Session;
        UIController UI;
        bool finish;
        DatabaseController dbController;

        public StudyController()
        {
            finish = false;
            Category = SelectedCategoryStore.SelectedCategory;
            dbController = new DatabaseController();
            UI = new UIController();
            Session = new StudySession()
            {
                CategoryId = Category.StackId,
                StudyDate = DateTime.Now
            };
        }

        public void BeginStudy()
        {
            foreach (var card in Category.FlashCards)
            {
                if (finish)
                {
                    break;
                }
                Session.QuestionsAnswered++;
                DisplayCard(card);

            }

            UI.DisplayInPanel(new[] {
                $"You answered {Session.QuestionsAnsweredCorrectly} correctly, out of {Session.QuestionsAnswered}",
                "Press any key to return to main menu"
            }, "Results");

            Console.ReadKey();
            dbController.AddStudySession(Session);
            UI.MainMenu();


        }

        public void DisplayCard(FlashCardDto card)
        {
            Console.Clear();
            UI.DisplayInPanel(new[] { card.Question });

            string input = Console.ReadLine();

            Console.Clear();
            if (input.ToLower() == card.Answer.ToLower())
            {

                UI.DisplayInPanel(new[] { "Correct! Press any key to continue or type FINISH to end session" });
                Session.QuestionsAnsweredCorrectly++;
            }
            else
            {
                string[] message = new[]
                {

                    $"Incorrect! The correct answer was {card.Answer}. ",
                    "Press any key to continue or type FINISH to end session"
                };
                UI.DisplayInPanel(message);
            }
            string selection = Console.ReadLine();

            if (selection.ToLower() == "finish")
            {
                finish = true;
            }

        }



    }
}
