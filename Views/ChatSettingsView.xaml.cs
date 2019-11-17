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
using WpfMessenger.ViewModels;

namespace WpfMessenger.Views
{
    /// <summary>
    /// Логика взаимодействия для ChatSettings.xaml
    /// </summary>
    public partial class ChatSettingsView : Window
    {
        ChatSettingsViewModel chatUserViewModel;

        public ChatSettingsView(ChatModel chat, UserModel user)
        {
            InitializeComponent();

            chatUserViewModel = new ChatSettingsViewModel(chat, user);

            DataContext = chatUserViewModel;

            chatUserViewModel.Closing += (s, e) => Close();
        }
    }
}
