using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfMessenger.ViewModels;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для ListOfUsers.xaml
    /// </summary>
    public partial class ListOfUsersView : Window
    {
        public ListOfUsersViewModel listOfUsersViewModel;

        public ListOfUsersView(ChatModel chat, UserModel user, List<UserModel> _users)
        {
            InitializeComponent();

            listOfUsersViewModel = new ListOfUsersViewModel(chat, user, _users);

            DataContext = listOfUsersViewModel;

            listOfUsersViewModel.Closing += (s, e) => Close();
        }

        private void ListOfContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listOfUsersViewModel.SelectedUsers = ListOfContacts.SelectedItems.Cast<UserModel>().ToList();
        }
    }
}
