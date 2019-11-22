using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WpfMessenger.DBConnection;
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

        ChatModel _selectedChat;

        private string _search;

        List<ChatModel> _chats;
        List<MessageViewModel> messageViewModels;

        ChatsRepository chatRepository;
        ChatUserRepository chatUserRepository;
        MessagesRepository messagesRepository;
        UsersRepository usersRepository;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Closing;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public MainViewModel(UserModel user)
        {
            User = user;

            ReloadChats();
        }

        List<ChatModel> GetChatsByUser(UserModel user)
        {
            List<ChatModel> chats  = new List<ChatModel>();

            using (MainDataBase dataBase = new MainDataBase())
            {
                chatUserRepository = new ChatUserRepository(dataBase);
                chatRepository = new ChatsRepository(dataBase);

                foreach (ChatUserModel chatUserModel in chatUserRepository.GetAll(i => i.UserId == user.Id))
                {
                    chats.Add(chatRepository.GetById((int)chatUserModel.ChatId));
                }
            }
            return chats;
        }
        void ReloadChats()
        {
            using (MainDataBase dataBase = new MainDataBase())
            {
                Chats = GetChatsByUser(User);

                if (Chats.Count > 0)
                {
                    SelectedChat = Chats[0];

                    MessageViewModels = new List<MessageViewModel>();

                    messagesRepository = new MessagesRepository(dataBase);
                    usersRepository = new UsersRepository(dataBase);

                    foreach (MessageModel model in messagesRepository.GetAll(i => i.ChatID == SelectedChat.Id))
                    {
                        MessageViewModels.Add(new MessageViewModel()
                        {
                            Fullname = usersRepository.GetById((int)model.SenderID).Fullname,
                            Sent = model.Sent,
                            Text = model.Text
                        });
                    }
                }
            }
        }
        void ReloadMessages()
        {
            using (MainDataBase dataBase = new MainDataBase())
            {
                messagesRepository = new MessagesRepository(dataBase);
                usersRepository = new UsersRepository(dataBase);

                MessageViewModels = new List<MessageViewModel>();

                foreach (MessageModel model in messagesRepository.GetAll(i => i.ChatID == SelectedChat.Id))
                {
                    MessageViewModels.Add(new MessageViewModel()
                    {
                        Fullname = usersRepository.GetById((int)model.SenderID).Fullname,
                        Sent = model.Sent,
                        Text = model.Text
                    });
                }
            }
        }

        public string ChatName { get; private set; }
        public string TextMessage { get; set; }

        public List<ChatModel> Chats
        {
            get { return _chats; }
            set
            {
                _chats = value;
                OnPropertyChanged(nameof(Chats));
            }
        }

        public List<MessageViewModel> MessageViewModels
        {
            get { return messageViewModels; }
            set
            {
                messageViewModels = value;
                OnPropertyChanged(nameof(MessageViewModels));
            }
        }

        public ChatModel SelectedChat
        {
            get { return _selectedChat; }
            set
            {
                _selectedChat = value;
                if (_selectedChat != null)
                {
                    ChatName = _selectedChat.Name;

                    ReloadMessages();
                }
                    
                OnPropertyChanged(nameof(SelectedChat));
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
                    Chats = GetChatsByUser(User).Where(i => i.Name.Contains(Search)).ToList();
                }
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

                        if(string.IsNullOrEmpty(TextMessage))
                        {
                            MessageBox.Show("Message Can`t Be Empty");
                            return;
                        }                     

                        using (MainDataBase dataBase = new MainDataBase())
                        {
                            messagesRepository = new MessagesRepository(dataBase);
                            usersRepository = new UsersRepository(dataBase);

                            MessageModel message = new MessageModel();
                            message.ChatID = SelectedChat.Id;
                            message.SenderID = User.Id;
                            message.Text = TextMessage;
                            message.Sent = DateTime.Now;

                            messagesRepository.Add(message);
                            dataBase.SaveChanges();
                        }

                        ReloadMessages();
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
                        userInfoView.ShowDialog();

                        ReloadChats();
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
                        chatSettings.ShowDialog();

                        ReloadChats();
                    }));
            }
        }
    }
}
