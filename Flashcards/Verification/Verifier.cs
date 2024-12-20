﻿using Flashcards.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Verification
{
    internal static class Verifier
    {
        public static bool VerifyInput(string input)
        {
            if (input == null || input == "") return false;
            else return true;
        }

        public static bool VerifyCategory(string input, ListStore store)
        {
            var categories = store.GetStacks();
            if (VerifyInput(input))
            {
                foreach (var category in categories)
                {
                    if (category.StackId == int.Parse(input))
                    {
                        return true;
                            
                    }
                }
            }
            else
            {
                return false;
            }
            return false;

        }
        
    }
}
