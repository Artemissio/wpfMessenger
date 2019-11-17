using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class ChatSettingsViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        ChatUserRepository chatUserRepository = ChatUserRepository.GetInstance();
        ChatsRepository chatsRepository = ChatsRepository.GetInstance();

        UserModel _user;
        UserModel _selectedUser;

        static UserModel _previousAdmin;

        ChatModel _chat;

        RelayCommand _back;
        RelayCommand _makeAdmin;
        RelayCommand _remove;
        RelayCommand _add;
        RelayCommand _edit;

        ObservableCollection<UserModel> _users;

        private string _name;
        public bool Validated = false;

        public event EventHandler Closing;

        public ChatSettingsViewModel(ChatModel chat, UserModel user)
        {
            _chat = chat;
            _user = user;

            _name = _chat.Name;

            if (_chat.Admin == _user)
                _previousAdmin = _user;
            else
                _previousAdmin = _chat.Admin;

            _users = new ObservableCollection<UserModel>(chatUserRepository.GetUsersByChat(_chat));
        }

        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
                    case "Name":
                        if (!DataValidation.ValidateTitle(Name))
                            message = "Chat Name should be letters and 3-30 symbols long";
                        break;
                }
                Validated = DataValidation.ValidateTitle(Name);
                return message;
            }
        }

        public string Error
        {
            get { return this[string.Empty]; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ObservableCollection<UserModel> Users
        {
            get { return new ObservableCollection<UserModel>(_users); }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public RelayCommand Edit
        {
            get
            {
                return _edit ??
                    (_edit = new RelayCommand(o =>
                    {
                        if (_chat.Admin != _user)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (!Validated)
                        {
                            MessageBox.Show("Form Is Invalid or has empty fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if(SelectedUser == null)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        chatsRepository.UpdateChat(_chat.Id, Name, _user);

                        MessageBox.Show("Chat Is Successfully Edited", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                        MainView mainView = new MainView(ref _user);
                        mainView.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand MakeAdmin
        {
            get
            {
                return _makeAdmin ??
                    (_makeAdmin = new RelayCommand(o =>
                    {
                        if(_chat.Admin != _user)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (SelectedUser == null)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (SelectedUser == _user)
                        {
                            MessageBox.Show("You are already an admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }

                        chatsRepository.UpdateChat(_chat.Id, Name, SelectedUser);

                        MessageBox.Show($"{SelectedUser.Nickname} Is Now Admin", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
                    }));
            }
        }

        public RelayCommand Remove
        {
            get
            {
                return _remove ??
                    (_remove = new RelayCommand(o =>
                    {
                        if (_chat.Admin != _user)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (SelectedUser == null)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (SelectedUser == _user)
                        {
                            MessageBox.Show("You cant remove yourself", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        chatUserRepository.Remove(SelectedUser);

                        MessageBox.Show($"{SelectedUser.Nickname} was successfully removed", "Ok", 
                            MessageBoxButton.OK, MessageBoxImage.Information);                  
                    }));
            }
        }

        public RelayCommand Add
        {
            get
            {
                return _add ??
                    (_add = new RelayCommand(o =>
                    {
                        if (_chat.Admin != _user)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        ListOfUsersView listOfUsersView = new ListOfUsersView(_chat, _user);
                        listOfUsersView.Show();

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
                        MainView mainView = new MainView(ref _user);
                        mainView.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}
