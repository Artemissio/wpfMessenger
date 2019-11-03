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
    /// Логика взаимодействия для ListOfUsers.xaml
    /// </summary>
    public partial class ListOfUsers : Window
    {
        UsersRepository _repository = UsersRepository.GetInstance();
        ChatModel _chat;

        public ListOfUsers(ChatModel chat)
        {
            InitializeComponent();

            _chat = chat;
            ListOfContacts.ItemsSource = _repository.GetUsers();
        }

        private void ListOfContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserModel user = (UserModel)ListOfContacts.SelectedItem;
            _chat.Members.Add(user);
            Close();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListOfContacts.ItemsSource = _repository.GetUsers()
                                                    .FindAll(u => u.Nickname.Contains(tbSearch.Text));
        }
    }
}
