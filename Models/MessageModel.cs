using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class MessageModel
    {
        static int _idGenerator = 0;
        public int ID { get; private set; }
        public string Text { get; }
        public DateTime Sent { get; }
        public UserModel Sender { get; }
        public int ChatID { get; private set; }
        public ChatModel Chat { get; }

        public MessageModel(string text, UserModel sender)
        {
            ID = _idGenerator++;
            Text = text;
            Sent = DateTime.Now;
            Sender = sender;
        }
        public MessageModel(string text, UserModel sender, ChatModel chat) : this(text, sender)
        {
            Chat = chat;
            ChatID = chat.Id;
        }
    }
}
