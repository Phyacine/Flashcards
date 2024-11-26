using Flashcards.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Stores
{
    internal class SelectedFlashCard
    {
        public static FlashCardDto CurrentCard { set; get; }

        public SelectedFlashCard(FlashCardDto flashcard)
        {
            CurrentCard = flashcard;
        }
    }
}
