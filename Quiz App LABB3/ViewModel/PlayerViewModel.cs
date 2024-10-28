using Quiz_App_LABB3.Command;
using System;
using System.Collections.Generic;
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

            TestData = "Start value:";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start(); // Gör att timern startar automatiskt vid start

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
            TestData += "x";
        }
    }
}
