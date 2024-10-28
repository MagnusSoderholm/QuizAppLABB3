using Quiz_App_LABB3.Model;
using Quiz_App_LABB3.ViewModel;
using System.Text;
using System.Windows;

namespace Quiz_App_LABB3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();


        }
    }
}