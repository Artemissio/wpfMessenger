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
    /// Логика взаимодействия для NewChatView.xaml
    /// </summary>
    public partial class NewChatView : Window
    {
        ChatsRepository chatsRepository = ChatsRepository.GetInstance();
        UsersRepository usersRepository = UsersRepository.GetInstance();
        UserModel _user;
        List<UserModel> selectedUsers;

        public NewChatView(UserModel user)
        {
            InitializeComponent();
            _user = user;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            ChatModel chat = new ChatModel(tbChatName.Text, _user);
            chat.Members = selectedUsers;

            chatsRepository.AddChat(chat);

            MainView mainView = new MainView(_user);
            mainView.Show();
            Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UserInfoView userInfoView = new UserInfoView(_user);
            userInfoView.Show();
            Close();
        }

        private void TbSearchContact_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListOfContacts.ItemsSource = usersRepository.GetUsers(tbSearchContact.Text);
        }

        private void ListOfContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUsers = new List<UserModel>();
            selectedUsers = (List<UserModel>)ListOfContacts.SelectedItems;
        }
    }
}
