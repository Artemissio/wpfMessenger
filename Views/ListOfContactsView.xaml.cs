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
    /// Логика взаимодействия для ListOfContactsView.xaml
    /// </summary>
    public partial class ListOfContactsView : Window
    {
        UserModel _user;

        public ListOfContactsView(UserModel user)
        {
            InitializeComponent();

            if (user != null)
            {
                _user = user;
                //foreach (UserModel model in _user.Contacts)
                //    ListOfContacts.Items.Add(model);
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UserInfoView userInfoView = new UserInfoView(_user);
            userInfoView.Show();
            Close();
        }

        private void BtnAddContact_Click(object sender, RoutedEventArgs e)
        {
            NewContactView newContactView = new NewContactView(_user);
            newContactView.Show();
            Close();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            _user = (UserModel)ListOfContacts.SelectedItem;

            if(_user != null)
            {
                ContactInfo contactInfoView = new ContactInfo(_user);
                contactInfoView.Show();
                Close();
            }
        }
    }
}
