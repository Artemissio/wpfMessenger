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

        List<UserModel> _users;
        List<UserModel> selectedUsers;
        List<UserModel> _givenUsers;

        RelayCommand _accept;
        RelayCommand _back;

        private string _search;

        public event EventHandler Closing;

        public ListOfUsersViewModel(ChatModel chat, UserModel user, List<UserModel> users)
        {
            _chat = chat;
            _user = user;

            _givenUsers = users;

            using (MainDataBase dataBase = new MainDataBase())
            {
                chatUserRepository = new ChatUserRepository(dataBase);
                usersRepository = new UsersRepository(dataBase);

                //List<ChatUserModel> chatUsers = chatUserRepository.GetAll(i => i.ChatId != _chat.Id).Distinct().ToList();

                //Users = new List<UserModel>();

                //foreach(ChatUserModel chatUserModel in chatUsers)
                //{
                //    Users.Add(usersRepository.GetById((int)chatUserModel.UserId));
                //}

                Users = usersRepository.GetAll().Except(_givenUsers).ToList();

                selectedUsers = new List<UserModel>();
            }
        }

        //List<UserModel> GetOtherUsers(ChatModel chat)
        //{
        //    List<UserModel> users = new List<UserModel>();

        //    using(MainDataBase dataBase = new MainDataBase())
        //    {
        //        chatUserRepository = new ChatUserRepository(dataBase);
        //        usersRepository = new UsersRepository(dataBase);

        //        foreach(ChatUserModel chatUser in chatUserRepository.GetAll(i => i.ChatId == chat.Id))
        //        {
        //            UserModel user = usersRepository.GetAll(i => i.Id == chatUser.UserId).Distinct().FirstOrDefault();
        //            users.Add(user);
        //        }
        //    }

        //    return users;
        //}


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

                Users = Users.Where(i => i.Name.Contains(Search) || i.Surname.Contains(Search) || i.Nickname.Contains(Search)).ToList();
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
