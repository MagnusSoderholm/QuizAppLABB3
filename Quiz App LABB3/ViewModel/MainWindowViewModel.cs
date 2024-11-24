using Quiz_App_LABB3.Command;
using Quiz_App_LABB3.Dialogs;
using Quiz_App_LABB3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Quiz_App_LABB3.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

       
        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }


        public ConfigurationViewModel ConfigurationViewModel { get; }

        public PlayerViewModel PlayerViewModel { get; }

        public CreateQuestionPackWindow CreateQuestionPackWindow { get; }




        private QuestionPackViewModel? _activePack;
        public QuestionPackViewModel? ActivePack 
        {
            get => _activePack;

            set 
            {

                _activePack = value;
                RaisePropertyChanged();
                //ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            } 
        }

        private QuestionPackViewModel? _newPack;
        public QuestionPackViewModel? NewPack
        {
            get => _newPack;

            set
            {

                _newPack = value;
                RaisePropertyChanged();
                //ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            }
        }


        public DelegateCommand AddNewPackCommand { get; }
        public DelegateCommand DeletePackCommand { get; }
        public DelegateCommand SetActivePackCommand { get; }
        public DelegateCommand UpdateButtonCommand { get; }
        public DelegateCommand CreateQuestionPackWindowCommand { get; }
        public DelegateCommand ShowPlayerViewCommand { get; }

        public DelegateCommand ShowConfigurationViewCommand { get; }

        public DelegateCommand ChooseNewPack { get; }

        public DelegateCommand SetFullScreenCommand { get; }

        


        public MainWindowViewModel()
        {
            

            AddNewPackCommand = new DelegateCommand(AddNewPack);
            DeletePackCommand = new DelegateCommand(DeletePack, CanDeletePack);
            SetActivePackCommand = new DelegateCommand(SetActivePack);
            
            ShowPlayerViewCommand = new DelegateCommand(ShowPlayerView);
            ShowConfigurationViewCommand = new DelegateCommand(ShowConfigurationView);
            SetFullScreenCommand = new DelegateCommand(FullScreen);

            CreateQuestionPackWindowCommand = new DelegateCommand(CreatePack);
            ActivePack = new QuestionPackViewModel(new QuestionPack($"Default Question Pack ({Difficulty.Medium})"));
            NewPack = new QuestionPackViewModel(new QuestionPack($"New Question Pack ({Difficulty.Medium})"));
            UpdateButtonCommand = new DelegateCommand(UpdateButton);
            ConfigurationViewModel = new ConfigurationViewModel(this);
            PlayerViewModel = new PlayerViewModel(this);

            Packs = new ObservableCollection<QuestionPackViewModel>();

            Packs.Add(new QuestionPackViewModel(new QuestionPack($"Default New Pack ({Difficulty.Medium})")));

            ActivePack = Packs.FirstOrDefault();
            ActivePack.Questions.Add(new Question("New Question", "", "", "", ""));
        }

        private void SetActivePack(object obj)
        {
            ActivePack = (QuestionPackViewModel)obj;
        }

        private bool CanDeletePack(object? arg) => Packs.Count > 1;
        

        private void DeletePack(object obj)
        {
           Packs.Remove(ActivePack);
            DeletePackCommand.RaiseCanExecuteChanged();
        }

        private void AddNewPack(object obj)
        {
     
            var newPack = new QuestionPackViewModel(new QuestionPack("New Pack", Difficulty.Medium, 30));
            newPack.Questions.Add(new Question("Sample Question", "Correct", "Incorrect1", "Incorrect2", "Incorrect3"));
            Packs.Add(newPack);
            ActivePack = newPack;

           
            DeletePackCommand.RaiseCanExecuteChanged();
            
        }

        private void CreatePack(object obj)
        {
            Packs.Add(new QuestionPackViewModel(new QuestionPack("Default Name", Difficulty.Medium, 30)));
            DeletePackCommand.RaiseCanExecuteChanged();
            var window = new CreateQuestionPackWindow();
            window.ShowDialog();
        }

        private void FullScreen(object obj)
        {
            if(obj is Window window)
            {
                SetFullScreen.Fullscreen(window);
            }
        }

        //private void RandomAnswers(object obj)
        //{
        //    List<string> RandomList = new List<string>
        //    {
        //        ActivePack.
        //    };
        //}

        private bool _isPlayerVisible = false;

        public bool IsPlayerVisible
        {
            get => _isPlayerVisible;
            set
            {
                if (_isPlayerVisible == value) return;

                _isPlayerVisible = value;
                RaisePropertyChanged(nameof(IsPlayerVisible));
                RaisePropertyChanged(nameof(IsConfigurationVisible));
            }
        }

        private bool _isConfigurationMode = true;
        public bool IsConfigurationVisible
        {
            get => _isConfigurationMode;
            set
            {
                if (_isConfigurationMode == value) return;

                _isConfigurationMode = value;
                RaisePropertyChanged(nameof(IsConfigurationVisible));
                RaisePropertyChanged(nameof(IsPlayerVisible));
            }
        }

        private void ShowPlayerView(object obj)
        {
            IsPlayerVisible = true;
            IsConfigurationVisible = false;

            PlayerViewModel.StartGame();

        }

        private void ShowConfigurationView(object ojb)
        {
            IsConfigurationVisible = true;
            IsPlayerVisible = false;
            
        }


        public async Task LoadDataAsync()
        {
            var JsonHandler = new Quiz_App_LABB3.Json();
            List<QuestionPack> loadedPacks = await JsonHandler.LoadQuestionPack();

            
            foreach (var pack in loadedPacks)
            {
                if (pack != null && !string.IsNullOrWhiteSpace(pack.Name))
                {
                    pack.Questions ??= new List<Question>(); 
                    Packs.Add(new QuestionPackViewModel(pack));
                }
                else
                {
                    Debug.WriteLine("Skipping invalid QuestionPack.");
                }
            }

            
            if (!Packs.Any())
            {
                var defaultPack = new QuestionPack("Default QuestionPack");
                Packs.Add(new QuestionPackViewModel(defaultPack));
            }

            ActivePack = Packs.FirstOrDefault();
        }


        public async Task SaveDataAsync()
        {
            var JsonHandler = new Quiz_App_LABB3.Json();

            
            List<QuestionPack> packsToSave = Packs
                .Where(viewModel => viewModel != null && viewModel.Questions.Any())  
                .Select(viewModel => new QuestionPack(
                    viewModel.Name,
                    viewModel.Difficulty,
                    viewModel.TimeLimitInSeconds)
                {
                    Questions = viewModel.Questions.ToList() 
                })
                .ToList();

            if (packsToSave.Any())
            {
                await JsonHandler.SaveQuestionPack(packsToSave); 
            }
            else
            {
                Debug.WriteLine("No valid QuestionPacks to save.");
            }
        }

       

        private void UpdateButton(object obj)
        {
            
            UpdateButtonCommand.RaiseCanExecuteChanged(); // Gör att när UpdateButton inte kan tyckas på mer så blir knappen "otryckbar"
        }
    }
}
