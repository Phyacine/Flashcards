using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards.Models
{
    internal class StudySession
    {
        public int CategoryId {  get; set; }

        public int QuestionsAnswered {  get; set; }
        public int QuestionsAnsweredCorrectly { get; set; }

        public DateTime StudyDate { get; set; }
    }
}
