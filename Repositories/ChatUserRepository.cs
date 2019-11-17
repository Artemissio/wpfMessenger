using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class ChatUserRepository
    {
        static ChatUserRepository _repository;
        ChatUserModel chatUser = new ChatUserModel();

        ChatsRepository _chatsRepository = ChatsRepository.GetInstance();
        UsersRepository _usersRepository = UsersRepository.GetInstance();

        List<ChatUserModel> _chat_users;

        ChatUserRepository()
        {
            _chat_users = new List<ChatUserModel>();
        }

        public static ChatUserRepository GetInstance()
        {
            if (_repository == null)
                _repository = new ChatUserRepository();
            return _repository;
        }

        public List<ChatUserModel> GetChatUsers()
        {
            return _chat_users;
        }

        public void Add(ChatModel chat, UserModel user)
        {
            chatUser = new ChatUserModel(chat, user);
            _chat_users.Add(chatUser);
        }

        public void Remove(ChatModel chat)
        {
            chatUser = GetChatUser(chat);
            _chat_users.Remove(chatUser);
        }

        public void Remove(UserModel user)
        {
            chatUser = GetChatUser(user);
            _chat_users.Remove(chatUser);
        }

        public List<ChatUserModel> GetChatUsers(UserModel user)
        {
            return _chat_users.Distinct().Where(i => i.UserId == user.Id).ToList();
        }

        public List<ChatUserModel> GetChatUsers(ChatModel chat)
        {
            return _chat_users.Distinct().Where(i => i.ChatId == chat.Id).ToList();
        }

        public List<ChatUserModel> GetRestChatUsers(ChatModel chat)
        {
            return _chat_users.Distinct().Where(i => i.ChatId != chat.Id).ToList();
        }

        ChatUserModel GetChatUser(ChatModel chat)
        {
            return _chat_users.FirstOrDefault(i => i.Chat == chat);
        }

        ChatUserModel GetChatUser(UserModel user)
        {
            return _chat_users.FirstOrDefault(i => i.User == user);
        }

        public List<ChatModel> GetChatsByUser(UserModel user)
        {
            List<ChatModel> chats = new List<ChatModel>();

            foreach(ChatUserModel chatUser in GetChatUsers(user))
            {
                chats.Add(chatUser.Chat);
            }

            return chats;
        }

        public List<UserModel> GetUsersByChat(ChatModel chat)
        {
            List<UserModel> users = new List<UserModel>();

            foreach(ChatUserModel chatUser in GetChatUsers(chat))
            {
                users.Add(chatUser.User);
            }
            return users;
        }

        public List<UserModel> GetRestUsersByChat(ChatModel chat)
        {
            List<UserModel> users = new List<UserModel>();

            foreach (ChatUserModel chatUser in GetRestChatUsers(chat))
            {
                users.Add(chatUser.User);
            }
            return users;
        }

        public List<UserModel> GetRestUsersByChat(ChatModel chat, string search)
        {
            List<UserModel> users = new List<UserModel>();

            foreach (ChatUserModel chatUser in GetRestChatUsers(chat))
            {
                users.Add(chatUser.User);
            }
            return users;
            //return users.Where(u => u.Nickname.Contains(search) || u.Name.Contains(search) || u.Surname.Contains(search)).ToList();
        }
    }
}