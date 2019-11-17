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
    public class ChatsRepository
    {
        static ChatsRepository _repository;

        List<ChatModel> _chats;

        ChatsRepository()
        {
            _chats = new List<ChatModel>();
        }

        public static ChatsRepository GetInstance()
        {
            if (_repository == null)
                _repository = new ChatsRepository();
            return _repository;
        }

        public ChatModel GetChat(string search)
        {
            return _chats.FirstOrDefault(c => c.Name == search);
        }

        public ChatModel GetChat(int id)
        {
            return _chats.FirstOrDefault(c => c.Id == id);
        }

        public void AddChat(ChatModel chat)
        {
            _chats.Add(chat);
        }

        public void UpdateChat(int id, string name, UserModel user)
        {
            ChatModel chat = _chats.FirstOrDefault(c => c.Id == id);

            chat.Name = name;
            chat.Admin = user;
        }

        public void RemoveChat(ChatModel chat)
        {
            if (chat != null)
                _chats.Remove(chat);
        }

        public List<ChatModel> GetChats()
        {
            return _chats;
        }       

        public List<ChatModel> GetChats(string name)
        {
            return _chats.ToList().FindAll(c => c.Name.Contains(name));
        }
    }
}