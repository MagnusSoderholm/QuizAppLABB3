using Quiz_App_LABB3.Command;
using Quiz_App_LABB3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Quiz_App_LABB3.ViewModel
{
    internal class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public DelegateCommand AddButtonCommand { get; }
        public DelegateCommand RemoveButtonCommand { get; }


        public QuestionPackViewModel? ActivePack => mainWindowViewModel.ActivePack; 


        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        //    VisibilityMode = Visibility.Hidden;
            IsEnabled = false;

            AddButtonCommand = new DelegateCommand(AddButton);
            RemoveButtonCommand = new DelegateCommand(RemoveButton, RemoveButtonActive);

            ActivePack.Questions.Add(new Question("Question abc", "a", "b", "c", "d"));
            SelectedItem = ActivePack.Questions.FirstOrDefault();


        }


        private bool RemoveButtonActive(object? arg)
        {

            if (IsEnabled) return true;
            else return false;
        }

        private void RemoveButton(object obj)
        {
            ActivePack.Questions.Remove(SelectedItem);

            RemoveButtonCommand.RaiseCanExecuteChanged();
        }

        private void AddButton(object obj)
        {
            //VisibilityMode = Visibility.Visible;
            ActivePack.Questions.Add(new Question(Query, CorrectAnswer, IncorrectAnswer1, IncorrectAnswer3, IncorrectAnswer3));

            AddButtonCommand.RaiseCanExecuteChanged();
        }


        private Question? _selectedItem;

        public Question? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }

        private Visibility _visability;

        public Visibility VisabilityMode
        {
            get => _visability;
            set
            {
                _visability = value;
                RaisePropertyChanged();
            }

        }


        private string _query;

        public string Query
        {
            get => SelectedItem.Query;
            set
            {
                SelectedItem.Query = value;
                RaisePropertyChanged();
                RaisePropertyChanged("SelectedItem");
                RaisePropertyChanged(nameof(ActivePack));
            }
        }

        private string _correctAnswer;

        public string CorrectAnswer
        {
            get => _correctAnswer;
            set
            {
                _correctAnswer = value;
                RaisePropertyChanged();
            }
        }

        private string _incorrectAnswer1;

        public string IncorrectAnswer1
        {
            get => _incorrectAnswer1;
            set
            {
                _incorrectAnswer1 = value;
                RaisePropertyChanged();
            }
        }

        private string _incorrectAnswer2;

        public string IncorrectAnswer2
        {
            get => _incorrectAnswer2;
            set
            {
                _incorrectAnswer2 = value;
                RaisePropertyChanged();
            }
        }

        private string _incorrectAnswer3;

        public string IncorrectAnswer3
        {
            get => _incorrectAnswer3;
            set
            {
                _incorrectAnswer3 = value;
                RaisePropertyChanged();
            }
        }

        private bool _isEnabled;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }


        private string _activePack;

        public string ActivePack
        {
            get => _activePack;
            set
            {
                _activePack = value;
                RaisePropertyChanged();
            }
        }


        public MainWindowViewModel()
        {
            ConfigurationViewModel = new ConfigurationViewModel(this);
            PlayerViewModel = new PlayerViewModel(this);

            ActivePack = new QuestionPackViewModel(new QuestionPack("Default Question Pack"));
        }

    }

}
