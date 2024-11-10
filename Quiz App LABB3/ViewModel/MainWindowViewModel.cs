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

namespace Quiz_App_LABB3.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
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
        public DelegateCommand CreateQuestionPackWindowCommand { get; }

        public DelegateCommand ShowPlayerViewCommand { get; }

        public DelegateCommand ShowConfigurationViewCommand { get; }

        public DelegateCommand ChooseNewPack { get; }

        public DelegateCommand SetFullScreenCommand { get; }


        public MainWindowViewModel()
        {
            

            AddNewPackCommand = new DelegateCommand(AddNewPack);
            DeletePackCommand = new DelegateCommand(DeletePack, CanDeletePack);
            CreateQuestionPackWindowCommand = new DelegateCommand(CreatePack);
            ShowPlayerViewCommand = new DelegateCommand(ShowPlayerView);
            ShowConfigurationViewCommand = new DelegateCommand(ShowConfigurationView);
            SetFullScreenCommand = new DelegateCommand(FullScreen);


           

            IsConfigurationVisible = true;
            IsPlayerVisible = false;



            ActivePack = new QuestionPackViewModel(new QuestionPack($"Default Question Pack ({Difficulty.Medium})"));
            NewPack = new QuestionPackViewModel(new QuestionPack($"New Question Pack ({Difficulty.Medium})"));

            ConfigurationViewModel = new ConfigurationViewModel(this);
            PlayerViewModel = new PlayerViewModel(this);

            Packs = new ObservableCollection<QuestionPackViewModel>();

            Packs.Add(new QuestionPackViewModel(new QuestionPack($"Default New Pack ({Difficulty.Medium})")));

            //ActivePack = Packs.FirstOrDefault();
            //ActivePack.Questions.Add(new Question("Question", "Answer1", "Answer2", "Answer3", "Answer4"));
        }

        private bool CanDeletePack(object? arg) => Packs.Count > 1;
        

        private void DeletePack(object obj)
        {
           Packs.Remove(ActivePack);
            DeletePackCommand.RaiseCanExecuteChanged();
        }

        private void AddNewPack(object obj)
        {

            Packs.Add(new QuestionPackViewModel(new QuestionPack("Default Name", Difficulty.Medium, 30)));
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

        private bool _isPlayerVisible;

        public bool IsPlayerVisible
        {
            get => _isPlayerVisible;
            set
            {
                if (_isPlayerVisible == value) return;

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
                if (_isPlayerVisible == !value) return;

                _isPlayerVisible = !value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(IsPlayerVisible));
            }
        }

        private void ShowPlayerView(object obj)
        {
            IsPlayerVisible = true;

        }

        private void ShowConfigurationView(object ojb)
        {
            IsConfigurationVisible = true;

        }

        
        //public async Task LoadDataAsync()
        //{
        //    var JsonHandler = new Quiz_App_LABB3.JSON.Json();
        //    List<QuestionPack> loadedPacks = await JsonHandler.LoadJson();

        //    foreach (var pack in loadedPacks)
        //    {
        //        Packs.Add(new QuestionPackViewModel(pack));
        //    }
        //    if (Packs.Any())
        //    {
        //        ActivePack = Packs.First();
        //    }

        //    ConfigurationViewModel.AddQuestionCommand.RaiseCanExecuteChanged();

        //}
        //public async Task SaveDataAsync()
        //{
        //    var JsonHandler = new Quiz_App_LABB3.JSON.json();

        //    List<QuestionPack> packsToSave = Packs.Select(ViewModel => new QuestionPack(
        //        viewModel.Name;
        //        viewModel.Difficulty;
        //        viewModel.TimeLimitInSeconds);
        //    {
        //        Questions = viewModel.Questions.ToList();
        //    }       
        //    ).ToList);

        //}


        //public async Task LoadPacks()
        //{
        //    var manager = new Json();
        //    Packs = await manager.LoadQuestionPack();

        //    if (Packs == null)
        //    {
        //        ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
        //        Packs.Add(ActivePack);
        //    }
        //    else
        //    {
        //        ActivePack = Packs[0];

        //    }
        //}




    }
}
