using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.NewFolder
{
    internal class FlashCard
    {
        public int StackId { get; set; }
        public int FlashCardId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
