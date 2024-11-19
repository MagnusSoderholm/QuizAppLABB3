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

            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private async void MainWindow_Closed(object? sender, EventArgs e)
        {

            await viewModel.SaveDataAsync();
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.LoadDataAsync();
        }

    }

}