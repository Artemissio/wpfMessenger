using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WpfMessenger.Repositories;
using System.Windows.Documents;
using WpfMessenger.Models;
using WpfMessenger.Interfaces;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfMessenger.Views;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        UsersRepository _repository = UsersRepository.GetInstance();
        UserModel _user;
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            _user = _repository.GetUser(tbNumber.Text, tbPassword.Text);

            if (_user != null)
            {
                MainView mainView = new MainView(_user);
                mainView.Show();
                Close();
            }
            MessageBox.Show("Login or Password Are Incorrect");
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
