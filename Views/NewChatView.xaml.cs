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
    /// Логика взаимодействия для NewChatView.xaml
    /// </summary>
    public partial class NewChatView : Window
    {
        NewChatViewModel newChatViewModel;

        public NewChatView(UserModel user)
        {
            InitializeComponent();

            newChatViewModel = new NewChatViewModel(user);

            DataContext = newChatViewModel;

            newChatViewModel.Closing += (s, e) => Close();
        }

        private void ListOfContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newChatViewModel.SelectedUsers = ListOfContacts.SelectedItems.Cast<UserModel>().ToList();
        }
    }
}
