using Quiz_App_LABB3.Model;
using Quiz_App_LABB3.ViewModel;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace Quiz_App_LABB3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new MainWindowViewModel();

            //Loaded += MainWindow_Loaded;

        }

        //private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
           
        //    await viewModel.LoadDataAsync();

        //    //throw new NotImplementedException();
        //}

        private Visibility _visability;

        public Visibility VisabilityMode
        {
            get => _visability;
            set
            {
                _visability = value;
               
            }
        }

        //private void ConfigurationView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{

        //}    



    }

}