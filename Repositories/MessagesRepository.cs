using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class MessagesRepository
    {
        static MessagesRepository _repository;

        List<MessageModel> _messages;

        MessagesRepository()
        {
            _messages = new List<MessageModel>();
        }

        public static MessagesRepository GetInstance()
        {
            if (_repository == null)
                _repository = new MessagesRepository();
            return _repository;
        }

        public List<MessageModel> GetMessages()
        {
            return _messages;
        }

        public List<MessageModel> GetMessages(ChatModel chat)
        {
            return _messages.Where(m => m.ChatID == chat.Id).ToList();
        }

        public void AddMessage(string text, UserModel user, ChatModel chat)
        {
            if(!string.IsNullOrEmpty(text) && user != null && chat != null)
            {
                MessageModel message = new MessageModel(text, user, chat);
                _messages.Add(message);
            }
        }

        public void RemoveMessage(ChatModel chat, MessageModel message)
        {
            if (chat != null)
                _messages.Remove(message);
        }
    }
}
