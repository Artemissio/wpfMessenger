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
using WpfMessenger.Repositories;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для RestorePasswordView.xaml
    /// </summary>
    public partial class RestorePasswordView : Window
    {
        UsersRepository _repository = UsersRepository.GetInstance();

        public RestorePasswordView()
        {
            InitializeComponent();
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginView = new LoginView();
            loginView.Show();
            Close();
        }
    }
}
