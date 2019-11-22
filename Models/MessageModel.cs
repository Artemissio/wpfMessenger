using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class MessageModel
    {
        public int ID { get; private set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
        public int? SenderID { get; set; }
        public UserModel Sender { get; set; }
        public int? ChatID { get; set; }
        public ChatModel Chat { get; set; }

        public MessageModel() { }
    }
}
