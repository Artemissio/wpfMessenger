using System.Windows;
using System.Windows.Controls;
using WpfMessenger.Models;
using WpfMessenger.ViewModels;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        MainViewModel mainViewModel;

        public MainView(ref UserModel user)
        {
            InitializeComponent();

            mainViewModel = new MainViewModel(user);

            DataContext = mainViewModel;
            mainViewModel.Closing += (s, e) => Close();
        }
    }
}