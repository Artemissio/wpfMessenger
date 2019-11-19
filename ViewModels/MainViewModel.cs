using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public UserModel User { get; set; }

        RelayCommand _exit;
        RelayCommand _send;
        RelayCommand _userInfo;
        RelayCommand _chatSettings;
        RelayCommand _refresh;

        ChatModel _selectedChat;

        private string _search;
        private string _chatName;

        ObservableCollection<ChatModel> _chats;
        ObservableCollection<MessageModel> _messages;

        ChatsRepository _chatsRepository = ChatsRepository.GetInstance();
        ChatUserRepository _chatUserRepository = ChatUserRepository.GetInstance();
        MessagesRepository _messagesRepository = MessagesRepository.GetInstance();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Closing;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainViewModel(UserModel user)
        {
            User = user;

            Chats = new ObservableCollection<ChatModel>(_chatUserRepository.GetChatsByUser(User));
        }

        public string ChatName { get; private set; }

        public ObservableCollection<ChatModel> Chats
        {
            get { return _chats; }
            set
            {
                _chats = new ObservableCollection<ChatModel>(_chatUserRepository.GetChatsByUser(User));
                OnPropertyChanged(nameof(Chats));
            }
        }

        public ChatModel SelectedChat
        {
            get { return _selectedChat; }
            set
            {
                _selectedChat = value;
                ChatName = _selectedChat.Name;
                OnPropertyChanged(nameof(SelectedChat));
            }
        }

        public ObservableCollection<MessageModel> Messages
        {
            get
            {
                if (SelectedChat != null)
                    return _messages;
                return null;
            }

            private set
            {
                //Messages = new ObservableCollection<MessageModel>(_messagesRepository.GetMessages(SelectedChat));

                _messages = new ObservableCollection<MessageModel>(_messagesRepository.GetMessages(SelectedChat));
                OnPropertyChanged(nameof(SelectedChat));
            }
        }

        public string TextMessage { get; set; }

        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));

                Chats = new ObservableCollection<ChatModel>(_chatsRepository.GetChats(_search));
            }
        }

        public RelayCommand Refresh
        {
            get
            {
                return _refresh ??
                    (_refresh = new RelayCommand(o =>
                    {
                        Chats = new ObservableCollection<ChatModel>(_chatUserRepository.GetChatsByUser(User));
                    }));
            }
        }

        public RelayCommand SendMessage
        {
            get
            {
                return _send ??
                    (_send = new RelayCommand(obj =>
                    {
                        if (SelectedChat == null)
                        {
                            MessageBox.Show("Choose Chat To Send Message");
                            return;
                        }

                        _messagesRepository.AddMessage(TextMessage, User, SelectedChat);

                        Messages = new ObservableCollection<MessageModel>(_messagesRepository.GetMessages(SelectedChat));
                    }));
            }
        }

        public RelayCommand Exit
        {
            get
            {
                return _exit ??
                    (_exit = new RelayCommand(obj =>
                    {
                        LoginView loginView = new LoginView();
                        loginView.Show();
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand UserInfo
        {
            get
            {
                return _userInfo ??
                    (_userInfo = new RelayCommand(obj =>
                    {
                        UserInfoView userInfoView = new UserInfoView(User);
                        userInfoView.Show();
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand ChatSettings
        {
            get
            {
                return _chatSettings ??
                    (_chatSettings = new RelayCommand(obj =>
                    {
                        if(SelectedChat == null)
                        {
                            MessageBox.Show("Choose Chat To Display");
                            return;
                        }

                        ChatSettingsView chatSettings = new ChatSettingsView(SelectedChat, User);
                        chatSettings.Show();
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}
