using Flashcards.Controllers;
using Flashcards.Helpers;
using Flashcards.Models;
using Flashcards.NewFolder;
using Flashcards.Verification;
using Flashcards.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Stores
{
    internal class ListStore
    {
        public List<FlashCard> Cards;
        public List<FlashCardDto> CardsDto;
        public static List<StackItem> Stacks;
        public DatabaseController dbAccess;
        public PopulateListController popLists;

        public ListStore()
        {
            popLists = new PopulateListController(Constants.ConnectionString);
            
            Cards = new List<FlashCard>();
            CardsDto = new List<FlashCardDto>();
            Stacks = new List<StackItem>();
            dbAccess = new DatabaseController();

            popLists.PopulateAll(Cards, CardsDto, Stacks);

        }
        public static string GetCategoryName(string id)
        {
            return Stacks.Find(a => a.StackId == int.Parse(id)).StackName;
        }
        public void GetHistory(string year)
        {
            Console.Clear();
            DataTable table = dbAccess.GetStudyHistory(year);

            UIController ui = new UIController();
            ui.DisplayHistory(table);

            DataTable table2 = dbAccess.GetStudyAverages(year);
            ui.DisplayHistory(table2);





        }

        public StackItem CurrentSelected()
        {
            return SelectedCategoryStore.SelectedCategory;
        }
        public List<StackItem> GetStacks()
        {
            return Stacks;
        }

        public void SetSelectedCategory(string categoryId)
        {
            SelectedCategoryStore.SelectedCategory = GetStack(categoryId);
        }
        public void SetSelectedCategoryByName(string categoryName)
        {
            SelectedCategoryStore.SelectedCategory = GetStackByCategory(categoryName);
        }

        public void SetSelectedCard(string cardId)
        {
            int id;
            try
            {
                id = int.Parse(cardId);
                var card = SelectedCategoryStore.SelectedCategory.FlashCards.Find(a => a.DtoId == id);
                SelectedFlashCard.CurrentCard = card;
            }
            catch { }
        }


        public StackItem GetStack(string id)
        {
            return Stacks.Find(a => a.StackId == int.Parse(id));
        }
        public StackItem GetStackByCategory(string category)
        {
            return Stacks.Find(a => a.StackName == category);
        }



        public bool Validate(string id)
        {
            int stackId;
            if (int.TryParse(id, out stackId))
            {
                if (Stacks.Find(a => a.StackId == stackId) != null)
                {
                    return true;
                }

            }
            return false;
        }
        //Stack actions -----------------------
        public void AddNewStack(string categoryName)
        {
            StackItem category = new StackItem()
            {
                StackName = categoryName
            };
            dbAccess.AddNewStackItem(category);
            Stacks.Add(dbAccess.GetStackByCategory(category.StackName));
            popLists.PopulateAll(Cards, CardsDto, Stacks);
        }
        public void RemoveStack()
        {
            var stack = SelectedCategoryStore.SelectedCategory;
            dbAccess.DeleteStack(stack);

            popLists.PopulateAll(Cards, CardsDto, Stacks);


        }

        /*public T Adwd<T>()
        {
            T value = (T)Convert.ChangeType(1, typeof(T));
            return value;
        }*/
        public void UpdateStackName(string name)
        {
            var stack = SelectedCategoryStore.SelectedCategory;
            foreach (var a in Stacks)
            {
                if (a.StackId == stack.StackId)
                {
                    a.StackName = name;
                    dbAccess.UpdateStackItem(a);
                    popLists.PopulateAll(Cards, CardsDto, Stacks);
                }
            }
        }
        public StackItem GetCategory(string categoryId)
        {
            if (Validate(categoryId))
            {
                return Stacks.Find(a => a.StackId == int.Parse(categoryId));
            }
            else return null;
        }
        //End Stack Actions-------

        //Card Actions---------------------

        public void AddNewCard(string question, string answer)
        {
            dbAccess.AddFlashCard(question, answer, SelectedCategoryStore.SelectedCategory);

            popLists.PopulateAll(Cards, CardsDto, Stacks);
            SelectedCategoryStore.SelectedCategory = GetCategory(SelectedCategoryStore.SelectedCategory.StackId.ToString());
        }
        public int RemoveCard()
        {

            dbAccess.RemoveFlashCard(SelectedFlashCard.CurrentCard);
            popLists.PopulateAll(Cards, CardsDto, Stacks);

            return SelectedCategoryStore.SelectedCategory.FlashCards.Count;
        }
    }
}
