using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfMessenger.DBConnection;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class ListOfUsersViewModel : INotifyPropertyChanged
    {
        ChatUserRepository chatUserRepository;
        UsersRepository usersRepository;

        ChatModel _chat;
        UserModel _user;

        ObservableCollection<UserModel> _users;
        ObservableCollection<UserModel> selectedUsers;
        ObservableCollection<UserModel> _givenUsers;

        RelayCommand _accept;
        RelayCommand _back;

        private string _search;

        public event EventHandler Closing;

        public ListOfUsersViewModel(ChatModel chat)
        {
            _chat = chat;

            Users = new ObservableCollection<UserModel>(GetRestUsers());

            selectedUsers = new ObservableCollection<UserModel>();
        }

        List<UserModel> GetRestUsers()
        {
            List<UserModel> restUsers = new List<UserModel>();

            using (MainDataBase dataBase = new MainDataBase())
            {
                chatUserRepository = new ChatUserRepository(dataBase);
                usersRepository = new UsersRepository(dataBase);

                _givenUsers = new ObservableCollection<UserModel>();

                List<ChatUserModel> chatUsers = chatUserRepository.GetAll(i => i.ChatId == _chat.Id);

                foreach (ChatUserModel chatUser in chatUsers)
                {
                    _givenUsers.Add(usersRepository.GetById((int)chatUser.UserId));
                }

                return restUsers = usersRepository.GetAll().Except(_givenUsers).ToList();
            }
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

                // does not work
                //Users = new ObservableCollection<UserModel>(GetRestUsers().Where(i => i.Name.Contains(Search)
                //                                                                || i.Surname.Contains(Search)
                //                                                                || i.Nickname.Contains(Search)));


                //Users.Where(i => i.Name.Contains(Search)
                //                    || i.Surname.Contains(Search)
                //                    || i.Nickname.Contains(Search));
                //Users = new ObservableCollection<UserModel>(Users.Where(i => i.Name.Contains(Search)
                //                                                            || i.Surname.Contains(Search)
                //                                                            || i.Nickname.Contains(Search)));
            }
        }

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public ObservableCollection<UserModel> SelectedUsers
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
                        using (MainDataBase dataBase = new MainDataBase())
                        {
                        
                            foreach (UserModel userModel in selectedUsers)
                            {
                                chatUserRepository = new ChatUserRepository(dataBase);

                                var chatUser = new ChatUserModel();
                                chatUser.ChatId = _chat.Id;
                                chatUser.UserId = userModel.Id;

                                chatUserRepository.Add(chatUser);
                            }

                            dataBase.SaveChanges();
                        }

                        MessageBox.Show("Users Successfully Added", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);

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
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}
