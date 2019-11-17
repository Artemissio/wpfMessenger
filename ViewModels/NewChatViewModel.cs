using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class NewChatViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        ChatsRepository chatsRepository = ChatsRepository.GetInstance();
        ChatUserRepository chatUserRepository = ChatUserRepository.GetInstance();
        UsersRepository usersRepository = UsersRepository.GetInstance();

        UserModel user;

        RelayCommand _accept;
        RelayCommand _back;

        List<UserModel> _users;
        List<UserModel> selectedUsers;

        private string _name;
        private string _search;

        public bool Validated = false;
        public event EventHandler Closing;

        public NewChatViewModel(UserModel user)
        {
            this.user = user;

            Users = usersRepository.GetUsers(Search, user).ToList();
            selectedUsers = new List<UserModel>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string Error
        {
            get { return this[string.Empty]; }
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

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));

                Users = usersRepository.GetUsers(Search, user).ToList();
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
                        if (!Validated)
                        {
                            MessageBox.Show("Form Is Invalid or has empty fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (SelectedUsers.Count() == 0)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        ChatModel chat = new ChatModel(Name, user);

                        chatsRepository.AddChat(chat);

                        chatUserRepository.Add(chat,user);

                        foreach (UserModel userModel in selectedUsers)
                        {
                            chatUserRepository.Add(chat, userModel);
                        }                                  

                        MessageBox.Show("Chat Is Successfully Created", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                        MainView mainView = new MainView(ref user);
                        mainView.Show();

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
                        UserInfoView userInfoView = new UserInfoView(user);
                        userInfoView.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}
