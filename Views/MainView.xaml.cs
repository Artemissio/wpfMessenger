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
using WpfMessenger.Interfaces;
using WpfMessenger.Models;
using WpfMessenger.Views;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using WpfMessenger.Repositories;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        ChatsRepository ChatsRepository = ChatsRepository.GetInstance();

        UserModel _user;
        ChatModel _chat;

        public MainView(UserModel user)
        {
            InitializeComponent();
            _user = user;
            //if (user != null)
            //{
            //    _user = user;
            //    foreach (ChatModel model in _user.Chats)
            //        ListOfChats.Items.Add(model);
            //}

            //ListOfChats.ItemsSource = _user.Chats;

            ListOfChats.ItemsSource = ChatsRepository.GetChats(_user);
            //ChatWindow.ItemsSource = _chat.Messages;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            string message = new TextRange(rtbMessage.Document.ContentStart,
                rtbMessage.Document.ContentEnd).Text;

            MessageModel messageModel = new MessageModel(message, _user);
            _chat.Messages.Add(messageModel);
        }

        private void BtnUserInfo_Click(object sender, RoutedEventArgs e)
        {
            UserInfoView userInfoView = new UserInfoView(_user);
            userInfoView.Show();
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Close();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            ChatSettings chatSettings = new ChatSettings(_chat, _user);
            chatSettings.Show();
        }

        private void ListOfChats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _chat = (ChatModel)ListOfChats.SelectedItem;

            if (_chat != null)
            {
                ChatWindow.ItemsSource = _chat.Messages;
            }
        }

        private void ChatWindow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListOfChats.ItemsSource = ChatsRepository.GetChats(tbSearch.Text);
        }
    }
}