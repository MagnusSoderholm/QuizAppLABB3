using Quiz_App_LABB3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz_App_LABB3.Model
{
    public class Question
    {
        [JsonConstructor]
        public Question(string query, string correctAnswer, List<string> incorrectAnswers)
        {
            Query = query;
            CorrectAnswer = correctAnswer;
            IncorrectAnswers = incorrectAnswers;

        }

        public Question(string query, string correctAnswer, string incorrectAnswer1, string incorrectAnswer2, string incorrectAnswer3)
        {
            Query = query;
            CorrectAnswer = correctAnswer;
            IncorrectAnswers = new List<string> { incorrectAnswer1, incorrectAnswer2, incorrectAnswer3 };
        }

        public string Query { get; set; }
        public string CorrectAnswer { get; set; } = new string("");
        public List<string> IncorrectAnswers { get; set; } = new List<string>();

    }

}
