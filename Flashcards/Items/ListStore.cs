using Flashcards.DatabaseActions;
using Flashcards.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Items
{
    internal class ListStore
    {
        public static List<FlashCard> Cards;
        public static List<FlashCardDto> CardsDto;
        public static List<StackItem> Stacks;

        
        
        public ListStore()
        {
            Cards = new List<FlashCard>();
            CardsDto = new List<FlashCardDto>();
            Stacks = new List<StackItem>();
            PopulateLists popLists = new PopulateLists(Constants.ConnectionString);
            popLists.PopulateAll(Cards, CardsDto, Stacks);
        }

    }
}
