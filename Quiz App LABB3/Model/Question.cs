using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App_LABB3.Model
{
    internal class Question
    {

        public Question(string queary, string correctAnswer,
            string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
        {
            Query = queary;
            CorrectAnswer = correctAnswer;
            IncorrectAnswers = new string[3] { incorrectAnswer1, incorrectAnswer2, incorrectAnswer3 };
        }

        public string Query { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] IncorrectAnswers { get; set; }
    }
}
