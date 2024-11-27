using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    internal class StudyHistory
    {
        Dictionary<string, int> StudyStats;

        public StudyHistory()
        {
            StudyStats = new Dictionary<string, int>();
        }
    }
}
