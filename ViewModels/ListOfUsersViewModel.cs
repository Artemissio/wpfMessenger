using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class ListOfUsersViewModel : INotifyPropertyChanged
    {
        ChatUserRepository chatUserRepository = ChatUserRepository.GetInstance();
        UsersRepository usersRepository = UsersRepository.GetInstance();

        ChatModel _chat;
        UserModel _user;

        List<UserModel> _users;
        List<UserModel> selectedUsers;

        RelayCommand _accept;
        RelayCommand _back;

        private string _search;

        public event EventHandler Closing;

        public ListOfUsersViewModel(ChatModel chat, UserModel user)
        {
            _chat = chat;
            _user = user;

            Users = chatUserRepository.GetRestUsersByChat(chat);
            selectedUsers = new List<UserModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));

                Users = chatUserRepository.GetRestUsersByChat(_chat, Search);
            }
        }

        public List<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public List<UserModel> SelectedUsers
        {
            get { return selectedUsers; }
            set
            {
                selectedUsers = value;
                OnPropertyChanged(nameof(SelectedUsers));
            }
        }

        public RelayCommand Accept
        {
            get
            {
                return _accept ??
                    (_accept = new RelayCommand(o =>
                    {

                        if (SelectedUsers.Count() == 0)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        foreach (UserModel userModel in selectedUsers)
                        {
                            chatUserRepository.Add(_chat, userModel);
                        }

                        MessageBox.Show("Users Successfully Added", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                        ChatSettingsView chatSettingsView = new ChatSettingsView(_chat, _user);
                        chatSettingsView.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
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
                        ChatSettingsView chatSettingsView = new ChatSettingsView(_chat, _user);
                        chatSettingsView.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}
