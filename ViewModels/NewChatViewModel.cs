using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfMessenger.DBConnection;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class NewChatViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        ChatsRepository chatsRepository;
        ChatUserRepository chatUserRepository;
        UsersRepository usersRepository;

        UserModel user;

        RelayCommand _accept;
        RelayCommand _back;

        ObservableCollection<UserModel> _users;
        ObservableCollection<UserModel> selectedUsers;

        private string _name;
        private string _search;

        public bool Validated = false;
        public event EventHandler Closing;

        public NewChatViewModel(UserModel user)
        {
            this.user = user;

            using (MainDataBase dataBase = new MainDataBase())
            {
                usersRepository = new UsersRepository(dataBase);

                Users = new ObservableCollection<UserModel>(usersRepository.GetAll(i => i.Id != user.Id));
                selectedUsers = new ObservableCollection<UserModel>();
            }
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

                using (MainDataBase dataBase = new MainDataBase())
                {
                    usersRepository = new UsersRepository(dataBase);
                    Users = new ObservableCollection<UserModel>(usersRepository.GetAll(i => (i.Name.Contains(Search)
                                                                                            || i.Surname.Contains(Search)
                                                                                            || i.Nickname.Contains(Search))
                                                                                            && i.Id != user.Id));
                }
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

                        using (MainDataBase dataBase = new MainDataBase())
                        {
                            chatsRepository = new ChatsRepository(dataBase);
                            chatUserRepository = new ChatUserRepository(dataBase);

                            ChatModel chat = new ChatModel();
                            chat.Name = Name;
                            chat.AdminID = user.Id;

                            //MessageBox.Show(chat.Name + " " + chat.AdminID); // ---------

                            //string chatUserResult = string.Empty;
                            //string chatResult = string.Empty;

                            chatsRepository.Add(chat);

                            ChatUserModel chatUser = new ChatUserModel();
                            chatUser.ChatId = chat.Id;
                            chatUser.UserId = user.Id;

                            //MessageBox.Show("Chat ID: " + chatUser.ChatId + " - user ID: " + chatUser.UserId); // ---------------

                            chatUserRepository.Add(chatUser); // added myself to ChatUsers

                            //dataBase.SaveChanges();

                            //chatUserResult = string.Empty;

                            //foreach (ChatUserModel chatUserModel in chatUserRepository.GetAll()) // displays me
                            //{
                            //    chatUserResult += $"Chat ID: {chatUserModel.ChatId} - User ID: {chatUserModel.UserId}\n";
                            //}

                            //MessageBox.Show(chatUserResult);

                            foreach (UserModel userModel in SelectedUsers)
                            {
                                chatUser = new ChatUserModel();
                                chatUser.ChatId = chat.Id;
                                chatUser.UserId = userModel.Id;

                                chatUserRepository.Add(chatUser); // added others to ChatUsers
                            }

                            dataBase.SaveChanges();
                        }
                        //chatUserResult = string.Empty;

                        //foreach (ChatUserModel chatUserModel in chatUserRepository.GetAll()) // displays all chat users (me + others)
                        //{
                        //    chatUserResult += $"Chat ID: {chatUserModel.ChatId} - User ID: {chatUserModel.UserId}\n";
                        //}

                        //foreach (ChatModel chatModel in chatsRepository.GetAll()) // displays all chats (1 for now)
                        //{
                        //    chatResult += $"Chat Name: {chatModel.Name} - Admin ID: {chatModel.AdminID}\n";
                        //}

                        //MessageBox.Show(chatResult);

                        MessageBox.Show("Chat Is Successfully Created", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);

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
