using Flashcards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Stores
{
    internal class SelectedCategoryStore
    {
        public static StackItem SelectedCategory { get; set; }

        public SelectedCategoryStore(StackItem category)
        {
            SelectedCategory = category;
        }
    }
}
