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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Quiz_App_LABB3.ViewModel
{
    public class ConfigurationViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;

        public ObservableCollection<QuestionPackViewModel> Packs { get => mainWindowViewModel.Packs; }

        public Visibility VisibilityMode { get; }
        public DelegateCommand AddQuestionCommand { get; }
        public DelegateCommand RemoveQuestionCommand { get; }
        public DelegateCommand EditOptionsCommand { get; }

        public DelegateCommand CreateQuestionPackWindowCommand { get; }
        


        public QuestionPackViewModel? ActivePack => mainWindowViewModel.ActivePack;



        public ConfigurationViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
           
            VisibilityMode = Visibility.Visible;
            IsEnabled = true;

            EditOptionsCommand = new DelegateCommand(EditOptions);


            IsConfigurationVisible = true;
            IsPlayerVisible = false;

            CreateQuestionPackWindowCommand = new DelegateCommand(CreatePack);
            AddQuestionCommand = new DelegateCommand(AddQuestionToActivePack);
            RemoveQuestionCommand = new DelegateCommand(RemoveQuestionFromActivePack);

            ActivePack.Questions.Add(new Question("New Question", "", "", "", ""));
            SelectedItem = ActivePack.Questions.FirstOrDefault();

            
        }

        private QuestionPack? _newQuestionPack;

        public QuestionPack? NewQuestionPack
        {
            get => _newQuestionPack;
            set
            {
                _newQuestionPack = value;
                RaisePropertyChanged(nameof(NewQuestionPack));
            }
        }
        private void RemoveQuestionFromActivePack(object parameter)
        {
            SelectedItem = ActivePack?.Questions.LastOrDefault();

            if (ActivePack != null && SelectedItem != null)
            {
                ActivePack.Questions.Remove(SelectedItem);
                RemoveQuestionCommand.RaiseCanExecuteChanged();
                mainWindowViewModel?.ShowPlayerViewCommand.RaiseCanExecuteChanged();
            }
            RaisePropertyChanged();
            RaisePropertyChanged(nameof(ActivePack));
        }

        private void AddQuestionToActivePack(object parameter)
        {
            var newQuestion = new Question(
            query: "New Question",
            correctAnswer: "",
            incorrectAnswer1: "",
            incorrectAnswer2: "",
            incorrectAnswer3: "");

            SelectedItem = newQuestion;

            ActivePack?.Questions.Add(newQuestion);

            RemoveQuestionCommand.RaiseCanExecuteChanged();

            mainWindowViewModel?.ShowPlayerViewCommand.RaiseCanExecuteChanged();

            RaisePropertyChanged(nameof(ActivePack));

        }
        private bool CanAddQuestionToActivePack(object parameter)
        {
            return ActivePack != null;
        }

        private void CreatePack(object? parameter)
        {
            var newPack = new QuestionPackViewModel(new QuestionPack(NewQuestionPack.Name, NewQuestionPack.Difficulty, NewQuestionPack.TimeLimitInSeconds));
            Packs.Add(newPack);

            mainWindowViewModel.ActivePack = newPack;
            RaisePropertyChanged(nameof(ActivePack));
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



    }

}
