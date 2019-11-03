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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfMessenger.Models;
using WpfMessenger.Repositories;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для NewContactView.xaml
    /// </summary>
    public partial class NewContactView : Window
    {
        UsersRepository _repository = UsersRepository.GetInstance();
        UserModel _user;

        public NewContactView(UserModel user)
        {
            InitializeComponent();   
            _user = user;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            //UserModel newUser = new UserModel();
            //_repository.AddUser(newUser);
            UserInfoView userInfoView = new UserInfoView(_user);
            userInfoView.Show();
            Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UserInfoView userInfoView = new UserInfoView(_user);
            userInfoView.Show();
            Close();
        }
    }
}
