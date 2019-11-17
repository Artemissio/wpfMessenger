using System.Windows;
using System.Windows.Input;
using WpfMessenger.ViewModels;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        RegistrationViewModel registrationViewModel = new RegistrationViewModel();

        public RegistrationView()
        {
            InitializeComponent();

            DataContext = registrationViewModel;
            registrationViewModel.Closing += (s, e) => Close();
        }

        private void TblLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {      
            LoginView loginView = new LoginView();
            loginView.Show();
            Close();
        }

        //private void BtnAccept_Click(object sender, RoutedEventArgs e)
        //{
        //    if (registrationViewModel.SignUp())
        //        Close();
        //}
    }
}
