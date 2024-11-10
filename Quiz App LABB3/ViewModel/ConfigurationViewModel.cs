using Quiz_App_LABB3.Command;
using Quiz_App_LABB3.Dialogs;
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

        public Visibility VisibilityMode { get; }
        public DelegateCommand AddQuestionCommand { get; }
        public DelegateCommand RemoveQuestionCommand { get; }
        public DelegateCommand EditOptionsCommand { get; }



        public QuestionPackViewModel? ActivePack => mainWindowViewModel.ActivePack;



        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
           
            VisibilityMode = Visibility.Visible;
            IsEnabled = true;

            EditOptionsCommand = new DelegateCommand(EditOptions);


            IsConfigurationVisible = true;
            IsPlayerVisible = false;


            AddQuestionCommand = new DelegateCommand(AddQuestion);
            RemoveQuestionCommand = new DelegateCommand(RemoveQuestion, RemoveQuestionActive);

            ActivePack.Questions.Add(new Question("New Question", "", "", "", ""));
            SelectedItem = ActivePack.Questions.FirstOrDefault();

            
        }


        private bool RemoveQuestionActive(object? arg)
        {

            if (IsEnabled) return true;
            else return false;
        }

        private void RemoveQuestion(object obj)
        {
            ActivePack.Questions.Remove(SelectedItem);

            RemoveQuestionCommand.RaiseCanExecuteChanged();
        }

        private void AddQuestion(object obj)
        {
            AddQuestion(obj, VisibilityMode);
        }

        private void AddQuestion(object obj, Visibility visibilityMode)
        {
            visibilityMode = Visibility.Visible;
            ActivePack.Questions.Add(new Question("New Question","","","",""));

            AddQuestionCommand.RaiseCanExecuteChanged();
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


        private void EditOptions(object obj)
        {
            var window = new EditQuestionPackWindow();
            window.ShowDialog();
        }


        //public MainWindowViewModel()
        //{
        //    ActivePack = new QuestionPackViewModel(new QuestionPack("Default Question Pack"));
        //    ConfigurationViewModel = new ConfigurationViewModel(this);
        //    PlayerViewModel = new PlayerViewModel(this);
        //}

    }

}
