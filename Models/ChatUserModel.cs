using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class ChatUserModel
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public ChatModel Chat { get; set; }
        public int ChatId { get; set; }
        public UserModel User { get; set; }
        public int UserId { get; set; }

        public ChatUserModel() { }

        public ChatUserModel(ChatModel chat, UserModel user)
        {
            Id = _idGenerator++;
            Chat = chat;
            User = user;

            ChatId = chat.Id;
            UserId = user.Id;
        }
    }
}