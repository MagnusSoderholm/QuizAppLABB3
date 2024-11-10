using Quiz_App_LABB3.Command;
using Quiz_App_LABB3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Quiz_App_LABB3.ViewModel
{

    internal class PlayerViewModel : ViewModelBase
    {

        private readonly MainWindowViewModel? mainWindowViewModel;

        private DispatcherTimer timer;
        private string _testData;

        private int _countdownValue = 30;

        public string TestData
        {
            get => _testData;
            private set
            {
                _testData = value;
                RaisePropertyChanged();
            }
        }



        public DelegateCommand UpdateButtonCommand { get; }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;



            IsConfigurationVisible = true;
            IsPlayerVisible = false;



            TestData = "{_countdownValue}";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start(); // Gör att timern startar automatiskt vid start

            UpdateButtonCommand = new DelegateCommand(UpdateButton, CanUpdateButton);
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
            //TestData += "x";
            //RaisePropertyChanged(TestData);

            _countdownValue--;

            TestData = $"{_countdownValue}";

            if (_countdownValue <= 0)
            {
                timer.Stop();
                TestData = "Next Question/Times up!";
            }
        }






        private bool _isPlayerVisible;

        public bool IsPlayerVisible
        {
            get => !_isPlayerVisible;
            set
            {

                _isPlayerVisible = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsConfigurationVisible));
            }
        }


        public bool IsConfigurationVisible
        {
            get => !_isPlayerVisible;
            set
            {

                _isPlayerVisible = !value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsPlayerVisible));
            }
        }











        private ObservableCollection<string> _allAnswers = new ObservableCollection<string>();
        ObservableCollection<string> AllAnswers
        {

            get { return _allAnswers; }

            set
            {
                _allAnswers = value;
                RaisePropertyChanged();
                RaisePropertyChanged("Answer1");
                RaisePropertyChanged("Answer2");
                RaisePropertyChanged("Answer3");
                RaisePropertyChanged("Answer4");
            }
        }
        private ObservableCollection<string> allAnswers = new ObservableCollection<string>();

        private string _answer1;
        public string Answer1 => AllAnswers.Count > 0 ? AllAnswers[0] : string.Empty;

        private string _answer2;
        public string Answer2 => AllAnswers.Count > 1 ? AllAnswers[1] : string.Empty;

        private string _answer3;
        public string Answer3 => AllAnswers.Count > 2 ? AllAnswers[2] : string.Empty;

        private string _answer4;
        public string Answer4 => AllAnswers.Count > 3 ? AllAnswers[3] : string.Empty;
















        public ObservableCollection<string> ShuffleAnswers(Question question)
        {
            for (int i = 0; i < question.IncorrectAnswers.Length; i++)
            {
                AllAnswers.Add(question.IncorrectAnswers[i]);
            }
            string correctAnswer = question.CorrectAnswer;

            AllAnswers.Add(question.CorrectAnswer);
            Shuffle(AllAnswers);
            RaisePropertyChanged("AllAnswers");
            return AllAnswers;
        }

        private void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
