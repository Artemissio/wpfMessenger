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

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для ChatSettings.xaml
    /// </summary>
    public partial class ChatSettings : Window
    {
        ChatModel _chat;
        UserModel _user;
        UserModel _selectedUser;
        public ChatSettings(ChatModel chat, UserModel user)
        {
            InitializeComponent();
            _chat = chat;
            _user = user;

            
            btnAddMember.Visibility = Visibility.Hidden;
            btnRemoveMember.Visibility = Visibility.Hidden;
            btnEdit.Visibility = Visibility.Hidden;

            if(_chat.Admin == _user)
            {
                _user.IsAdmin = true;
                btnEdit.Visibility = Visibility.Visible;
            }
        }

        private void ListOfContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedUser = (UserModel)ListOfContacts.SelectedItem;
        }

        private void BtnRemoveMember_Click(object sender, RoutedEventArgs e)
        {
            _chat.Members.Remove(_selectedUser);
        }

        private void BtnAddMember_Click(object sender, RoutedEventArgs e)
        {
            ListOfUsers listOfUsers = new ListOfUsers(_chat);
            listOfUsers.Show();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            _chat.Name = tbChatName.Text;
        }

        private void BtnMakeAdmin_Click(object sender, RoutedEventArgs e)
        { 
            _user.IsAdmin = false;
            _chat.Admin = _selectedUser;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
