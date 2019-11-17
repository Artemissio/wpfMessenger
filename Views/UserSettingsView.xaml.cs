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
using WpfMessenger.ViewModels;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : Window
    {

        UserSettingsViewModel userSettingsViewModel;

        public UserSettingsView(UserModel user)
        {
            InitializeComponent();
            userSettingsViewModel = new UserSettingsViewModel(user);

            DataContext = userSettingsViewModel;

            userSettingsViewModel.Closing += (s, e) => Close();
        }
    }
}
