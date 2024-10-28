using Quiz_App_LABB3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_App_LABB3.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }


        public ConfigurationViewModel ConfigurationViewModel { get; }

        public PlayerViewModel PlayerViewModel { get; }



        private QuestionPackViewModel? _activePack;
        public QuestionPackViewModel? ActivePack 
        {
            get => _activePack;

            set 
            {

                _activePack = value;
                RaisePropertyChanged();
                ConfigurationViewModel.RaisePropertyChanged("ActivePack");
            } 
        }

        public MainWindowViewModel()
        {

            ConfigurationViewModel = new ConfigurationViewModel(this);
            PlayerViewModel = new PlayerViewModel(this);


            ActivePack = new QuestionPackViewModel(new QuestionPack("My Question Pack"));
        }
    }
}
