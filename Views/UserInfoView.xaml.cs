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
    /// Логика взаимодействия для UserInfoView.xaml
    /// </summary>
    public partial class UserInfoView : Window
    {
        UserModel _user;

        public UserInfoView(UserModel user)
        {
            InitializeComponent();
            _user = user;
        }

        private void BtnNewChat_Click(object sender, RoutedEventArgs e)
        {
            NewChatView newChatView = new NewChatView(_user);
            newChatView.Show();
            Close();
        }

        //private void BtnNewContact_Click(object sender, RoutedEventArgs e)
        //{
        //    NewContactView newContactView = new NewContactView(_user);
        //    newContactView.Show();
        //    Close();
        //}

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            UserSettingsView userSettingsView = new UserSettingsView(_user);
            userSettingsView.Show();
            Close();
        }

        //private void BtnContacts_Click(object sender, RoutedEventArgs e)
        //{
        //    ListOfContactsView listOfContactsView = new ListOfContactsView(_user);
        //    listOfContactsView.Show();
        //    Close();
        //}

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
