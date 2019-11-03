using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class ChatsRepository
    {
        static ChatsRepository _repository;

        ChatModel _chat;
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
            for (int i = 0; i < _chats.Count; i++)
            {
                if (_chats[i].Name == search)
                    _chat = _chats[i];
            }
            return _chat;
        }

        public IEnumerable<ChatModel> GetChats()
        {
            return _chats;
        }
        public IEnumerable<ChatModel> GetChats(string name)
        {
            return _chats.FindAll(c => c.Name.Contains(name));
        }
        public IEnumerable<ChatModel> GetChats(UserModel user)
        {
            return _chats.FindAll(c => c.Members.Contains(user));
        }
    }
}