using System.Windows;
using System.Windows.Input;
using WpfMessenger.ViewModels;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        LoginViewModel loginViewModel = new LoginViewModel();
        public LoginView()
        {
            InitializeComponent();
            DataContext = loginViewModel;

            loginViewModel.Closing += (s, e) => Close();
        }

        private void TblForgotPassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RestorePasswordView restorePasswordView = new RestorePasswordView();
            restorePasswordView.Show();
            Close();
        }

        private void TblRegistration_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegistrationView registrationView = new RegistrationView();
            registrationView.Show();
            Close();
        }
    }
}
