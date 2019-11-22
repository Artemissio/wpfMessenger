using System;
using WpfMessenger.Models;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class UserInfoViewModel
    {
        UserModel user;

        RelayCommand _newChat;
        RelayCommand _settings;
        RelayCommand _back;

        public event EventHandler Closing;

        public string Fullname { get; }
        public string Number { get; }

        public UserInfoViewModel(UserModel user)
        {
            this.user = user;

            Fullname = this.user.Fullname;
            Number = this.user.Number;
        }

        public RelayCommand NewChat
        {
            get
            {
                return _newChat ??
                    (_newChat = new RelayCommand(obj =>
                    {
                        NewChatView newChatView = new NewChatView(user);
                        newChatView.ShowDialog();
                    }));
            }
        }

        public RelayCommand Settings
        {
            get
            {
                return _settings ??
                    (_settings = new RelayCommand(obj =>
                    {
                        UserSettingsView userSettingsView = new UserSettingsView(user);
                        userSettingsView.ShowDialog();
                    }));
            }
        }

        public RelayCommand Back
        {
            get
            {
                return _back ??
                    (_back = new RelayCommand(obj =>
                    {
                        Closing?.Invoke(this, EventArgs.Empty);                      
                    }));
            }
        }
    }
}