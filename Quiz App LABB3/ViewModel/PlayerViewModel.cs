using Quiz_App_LABB3.Command;
using Quiz_App_LABB3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Quiz_App_LABB3.ViewModel
{

    public class PlayerViewModel : ViewModelBase
    {

        public DelegateCommand AnswerCommand { get; }

        public DelegateCommand UpdateButtonCommand { get; }

        private readonly MainWindowViewModel? _mainWindowViewModel;
        public QuestionPackViewModel? ActivePack { get => _mainWindowViewModel?.ActivePack; }

        private DispatcherTimer _timer;
        
        private string _testData;

        private int _countdownValue;

        private int _currentQuestionIndex;

        public Question? CurrentQuestion => ActivePack?.Questions.ElementAtOrDefault(CurrentQuestionIndex);

        private string _questionText;

        private string _answer1;

        private string _answer2;

        private string _answer3;

        private string _answer4;

        public PlayerViewModel()
        {
            
            CurrentQuestionIndex = 0; // Starta på första frågan
        }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this._mainWindowViewModel = mainWindowViewModel;


            TestData = "{_countdownValue}";

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;

            AnswerCommand = new DelegateCommand(SetAnswerButton);
            UpdateButtonCommand = new DelegateCommand(UpdateButton, CanUpdateButton);
        }

        public string QuestionText
        {
            get => _questionText;
            set
            {
                _questionText = value;
                RaisePropertyChanged();
            }
        }

        public string Answer1
        {
            get => _answer1;
            set
            {
                _answer1 = value;
                RaisePropertyChanged();
            }
        }

        public string Answer2
        {
            get => _answer2;
            set
            {
                _answer2 = value;
                RaisePropertyChanged();
            }
        }

        public string Answer3
        {
            get => _answer3;
            set
            {
                _answer3 = value;
                RaisePropertyChanged();
            }
        }

        public string Answer4
        {
            get => _answer4;
            set
            {
                _answer4 = value;
                RaisePropertyChanged();
            }
        }

        public int CurrentQuestionIndex
        {
            get => _currentQuestionIndex;
            set
            {
                _currentQuestionIndex = value;
                RaisePropertyChanged();
                DisplayNextQuestion(); // Uppdatera frågan varje gång index ändras
            }
        }

        public string TestData
        {
            get => _testData;
            private set
            {
                _testData = value;
                RaisePropertyChanged();
            }
        }


        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                RaisePropertyChanged();
            }
        }


        private bool CanUpdateButton(object? arg)
        {
            return TestData.Length < 20; // Gör att TestData endast kan klickas ett visst antal gånger
        }

        private void UpdateButton(object obj)
        {
            TestData += "x";
            UpdateButtonCommand.RaiseCanExecuteChanged(); // Gör att när UpdateButton inte kan tyckas på mer så blir knappen "otryckbar"
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            // Uppdatera tiden och bind den till UI:et.
            if (TimeRemaining > 0)
            {
                TimeRemaining--; // Minska tid
            }
            else
            {
                _timer.Stop(); // Stoppa timern

                // Om det finns fler frågor, gå vidare, annars avsluta quizen.
                if (CurrentQuestionIndex < (ActivePack?.Questions.Count ?? 0) - 1)
                {
                    MessageBox.Show("Time is up! Next question", "Time is up!", MessageBoxButton.OK, MessageBoxImage.Information);
                    GoToNextQuestion();

                    // Återställ och starta timern för nästa fråga
                    ResetTimer();
                }
                else
                {
                    // Quiz är klart
                    MessageBox.Show("You've completed the quiz!", "Quiz Finished", MessageBoxButton.OK, MessageBoxImage.Information);
                    _mainWindowViewModel.IsPlayerVisible = false;  // Dölj spelaren
                    _mainWindowViewModel.IsConfigurationVisible = true; // Visa konfiguration
                }
            }
        }

        private void ResetTimer()
        {
            TimeRemaining = ActivePack?.TimeLimitInSeconds ?? 0; // InitialTime är startvärdet för varje fråga.
            _timer.Start();
        }


        public string ShowRemainingTime
        {
            get
            {
                return TimeSpan.FromSeconds(TimeRemaining).ToString(@"mm\:ss");
            }
        }

        public int TimeRemaining
        {
            get => _countdownValue;
            set
            {
                _countdownValue = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(ShowRemainingTime));
            }
        }


        public void StartGame()
        {
            if (ActivePack?.Questions.Count > 0)
            {
                CurrentQuestionIndex = 0; // Starta på första frågan
            }

            TimeRemaining = ActivePack?.TimeLimitInSeconds ?? 0;
            _timer.Start();
        }



        private void DisplayNextQuestion()
        {
            if (ActivePack?.Questions == null || CurrentQuestionIndex >= ActivePack.Questions.Count)
            {
                Debug.WriteLine("No more questions!");
                _timer.Stop();
                MessageBox.Show("You've completed the quiz!", "Quiz Finished", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var currentQuestion = ActivePack.Questions[CurrentQuestionIndex];
            QuestionText = currentQuestion.Query;
            RandomizeAnswers(currentQuestion);
        }



        private void RandomizeAnswers(Question question)
        {
            var answers = new List<string> { question.CorrectAnswer, question.IncorrectAnswers[0], question.IncorrectAnswers[1], question.IncorrectAnswers[2] };
            var randomizedAnswers = answers.OrderBy(a => Guid.NewGuid()).ToList();

            Answer1 = randomizedAnswers[0];
            Answer2 = randomizedAnswers[1];
            Answer3 = randomizedAnswers[2];
            Answer4 = randomizedAnswers[3];
        }

        private async void SetAnswerButton(object? obj)
        {
            UpdateButtonContent(CurrentQuestion.CorrectAnswer, "Correct!");

            if (obj is not string selectedAnswer)
                return;


            if (selectedAnswer == CurrentQuestion.CorrectAnswer)
            {
                UpdateButtonContent(selectedAnswer, "Correct!");
                Score++;
                await Task.Delay(2000);
                RaisePropertyChanged(nameof(Score));
                ResetTimer();
            }
            else
            {
                UpdateButtonContent(selectedAnswer, "Wrong!");
                await Task.Delay(2000);
                ResetTimer();
            }
            GoToNextQuestion();
        }

        private void UpdateButtonContent(string answer, string rightOrWrong)
        {
            if (Answer1 == answer)
            {
                Answer1 = rightOrWrong;
            }
            if (Answer2 == answer)
            {
                Answer2 = rightOrWrong;
            }
            if (Answer3 == answer)
            {
                Answer3 = rightOrWrong;
            }
            if (Answer4 == answer)
            {
                Answer4 = rightOrWrong;
            }

            RaisePropertyChanged(nameof(Answer1));
            RaisePropertyChanged(nameof(Answer2));
            RaisePropertyChanged(nameof(Answer3));
            RaisePropertyChanged(nameof(Answer4));
        }

        public void GoToNextQuestion()
        {
            if (ActivePack?.Questions != null && CurrentQuestionIndex < ActivePack.Questions.Count - 1)
            {
                CurrentQuestionIndex++; // Move to the next question
            }
            else
            {
                // Quiz is completed
                MessageBox.Show($"Quiz Finished! Your score: {Score}/{ActivePack?.Questions.Count}", "Quiz Complete", MessageBoxButton.OK, MessageBoxImage.Information);
                _mainWindowViewModel.IsPlayerVisible = false;  // Hide player view
                _mainWindowViewModel.IsConfigurationVisible = true; // Show configuration view
            }
        }


    }
}
