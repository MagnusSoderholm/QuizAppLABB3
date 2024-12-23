﻿using Quiz_App_LABB3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Quiz_App_LABB3.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Difficulty { Easy, Medium, Hard };

    public class QuestionPack
    {

        public string Name { get; set; }

        public Difficulty Difficulty { get; set; }

        public int TimeLimitInSeconds { get; set; }

        public List<Question> Questions { get; set; }

        public QuestionPack(string name, Difficulty difficulty = Difficulty.Medium, int timeLimitInSeconds = 30)
        {
            Name = name;
            Difficulty = difficulty;
            TimeLimitInSeconds = timeLimitInSeconds;
            Questions = new List<Question>();
        }

    }
}
