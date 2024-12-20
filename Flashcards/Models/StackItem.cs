﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flashcards.NewFolder;

namespace Flashcards.Models
{
    internal class StackItem
    {
        public string StackName { get; set; }
        public int StackId { get; set; }

        public List<FlashCardDto> FlashCards { get; set; }

        public StackItem()
        {
            FlashCards = new List<FlashCardDto>();
        }
    }
}
