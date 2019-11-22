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
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class ChatSettingsViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        UserModel _user;
        UserModel _selectedUser;

        static UserModel _previousAdmin;

        ChatModel _chat;

        RelayCommand _back;
        RelayCommand _makeAdmin;
        RelayCommand _remove;
        RelayCommand _add;
        RelayCommand _edit;
        RelayCommand _showProfile;
        RelayCommand _leaveChat;
        RelayCommand _deleteChat;

        ChatsRepository chatRepository;
        UsersRepository usersRepository;
        ChatUserRepository chatUserRepository;

        ObservableCollection<UserModel> _users;

        private string _name;
        public bool Validated = false;

        public event EventHandler Closing;

        public ChatSettingsViewModel(ChatModel chat, UserModel user)
        {
            _chat = chat;
            _user = user;

            _name = _chat.Name;
            using (MainDataBase dataBase = new MainDataBase())
            {
                usersRepository = new UsersRepository(dataBase);

                if (_chat.AdminID == _user.Id)
                    _previousAdmin = _user;
                else
                    _previousAdmin = usersRepository.GetById(_chat.AdminID);

                Users = new ObservableCollection<UserModel>(GetUsersByChat(_chat));
            }
        }

        List<UserModel> GetUsersByChat(ChatModel chat)
        {
            List<UserModel> users = new List<UserModel>();

            using (MainDataBase dataBase = new MainDataBase())
            {
                chatUserRepository = new ChatUserRepository(dataBase);
                usersRepository = new UsersRepository(dataBase);

                foreach (ChatUserModel chatUserModel in chatUserRepository.GetAll(i => i.ChatId == chat.Id))
                {
                    users.Add(usersRepository.GetById((int)chatUserModel.UserId));
                }
            }
            return users;
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

        #region PropertyChanged
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
        #endregion

        #region RelayCommands
        public RelayCommand Edit
        {
            get
            {
                return _edit ??
                    (_edit = new RelayCommand(o =>
                    {
                        if (_chat.AdminID != _user.Id)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (!Validated)
                        {
                            MessageBox.Show("Form Is Invalid or has empty fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        _chat.Name = Name;

                        using (MainDataBase dataBase = new MainDataBase())
                        {
                            chatRepository = new ChatsRepository(dataBase);

                            chatRepository.Edit(_chat);
                            dataBase.SaveChanges();
                        }
                        MessageBox.Show("Chat Is Successfully Renamed", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        if (SelectedUser == null)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (_chat.AdminID != _user.Id)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (_chat.AdminID == SelectedUser.Id)
                        {
                            MessageBox.Show("You are already an admin", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }                     

                        using (MainDataBase dataBase = new MainDataBase())
                        {
                            chatRepository = new ChatsRepository(dataBase);

                            _chat.AdminID = SelectedUser.Id;

                            chatRepository.Edit(_chat);
                            dataBase.SaveChanges();
                        }

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
                        if (SelectedUser == null)
                        {
                            MessageBox.Show("Choose at least one user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (_chat.AdminID != _user.Id)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }                       

                        if (SelectedUser.Id == _user.Id)
                        {
                            MessageBox.Show("You cant remove yourself", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        using (MainDataBase dataBase = new MainDataBase())
                        {
                            chatRepository = new ChatsRepository(dataBase);
                            chatUserRepository = new ChatUserRepository(dataBase);

                            ChatUserModel chatUserModel = chatUserRepository.GetAll(i => i.ChatId == _chat.Id &&
                                                                                     i.UserId == SelectedUser.Id).FirstOrDefault();

                            chatUserRepository.Delete(chatUserModel);
                            dataBase.SaveChanges();

                            MessageBox.Show($"{SelectedUser.Nickname} was successfully removed", "Ok",
                                MessageBoxButton.OK, MessageBoxImage.Information);

                            Users = new ObservableCollection<UserModel>(GetUsersByChat(_chat));
                        }
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
                        if (_chat.AdminID != _user.Id)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        ListOfUsersView listOfUsersView = new ListOfUsersView(_chat, _user, Users.ToList());
                        listOfUsersView.ShowDialog();

                        Users = new ObservableCollection<UserModel>(GetUsersByChat(_chat));
                    }));
            }
        }

        public RelayCommand ShowProfile
        {
            get
            {
                return _showProfile ??
                    (_showProfile = new RelayCommand(o =>
                    {
                        if(SelectedUser == null)
                        {
                            MessageBox.Show("Choose user", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        UserProfile userProfile = new UserProfile(SelectedUser);
                        userProfile.ShowDialog();
                    }));
            }
        }

        public RelayCommand LeaveChat
        {
            get
            {
                return _leaveChat ??
                    (_leaveChat = new RelayCommand(o =>
                    {
                        using (MainDataBase dataBase = new MainDataBase())
                        {
                            if (_chat.AdminID == _user.Id)
                            {
                                MessageBox.Show("You can`t leave chat if you are its admin.\n Make somebody else Admin and then leave", 
                                                "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            chatUserRepository = new ChatUserRepository(dataBase);

                            ChatUserModel chatUserModel = chatUserRepository.GetAll(i => i.ChatId == _chat.Id && 
                                                                                    i.UserId == _user.Id).FirstOrDefault();

                            chatUserRepository.Delete(chatUserModel);
                            dataBase.SaveChanges();
                        }
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand DeleteChat
        {
            get
            {
                return _deleteChat ??
                    (_deleteChat = new RelayCommand(o =>
                    {
                        if (_chat.AdminID != _user.Id)
                        {
                            MessageBox.Show("You are not admin", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        using(MainDataBase dataBase = new MainDataBase())
                        {                 
                            chatUserRepository = new ChatUserRepository(dataBase);

                            foreach (ChatUserModel chatUserModel in chatUserRepository.GetAll(i => i.ChatId == _chat.Id))
                            {
                                chatUserRepository.Delete(chatUserModel);
                                dataBase.SaveChanges();
                            }
                        }

                        using(MainDataBase context = new MainDataBase())
                        {
                            MessagesRepository messagesRepository = new MessagesRepository(context);

                            foreach (MessageModel messageModel in messagesRepository.GetAll(i => i.ChatID == _chat.Id))
                            {
                                messagesRepository.Delete(messageModel);
                                context.SaveChanges();
                            }
                        }

                        using(MainDataBase main = new MainDataBase())
                        {
                            chatRepository = new ChatsRepository(main);

                            ChatModel chat = chatRepository.GetById(_chat.Id);

                            chatRepository.Delete(chat);
                            main.SaveChanges();
                        }


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
        #endregion
    }
}
