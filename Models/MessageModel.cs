using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class MessageModel
    {
        public string Text { get; }
        public DateTime Sent { get; }
        public UserModel Sender { get; }
        public UserModel Recipient { get; }

        public MessageModel(string text, UserModel sender)
        {
            Text = text;
            Sent = DateTime.Now;
            Sender = sender;
        }
        public MessageModel(string text, UserModel sender, UserModel recipient) : this(text, sender)
        {
            Recipient = recipient;
        }
    }
}
