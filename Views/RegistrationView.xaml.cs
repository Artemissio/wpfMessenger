using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using WpfMessenger.Views;
using WpfMessenger.Interfaces;
using WpfMessenger.Models;
using WpfMessenger.Validation;
using WpfMessenger.Repositories;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        UserModel _user; 
        DataValidation DataValidation;
        UsersRepository _repository = UsersRepository.GetInstance();

        public RegistrationView()
        {
            InitializeComponent();
        }

        private void TblLogin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {      
            LoginView loginView = new LoginView();
            loginView.Show();
            Close();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            string surname = tbSurname.Text;
            string number = tbNumber.Text;
            string nickname = tbNickname.Text;
            string password = tbPassword.Text;
            string confirmPassword = tbConfirmPassword.Text;

            if(password != confirmPassword)
            {
                MessageBox.Show("Password Are Not The Same");
            }

            _user = new UserModel(name, surname, number, nickname, password, false);

            //DataValidation = new DataValidation(_user);

            //if (DataValidation.Validate())
            //{
                _repository.AddUser(_user);
                MainView mainView = new MainView(_user);
            mainView.Show();
                Close();
            //}
        }
    }
}
