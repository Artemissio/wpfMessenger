using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class ChatUserModel
    {
        public int Id { get; private set; }    
        public int? ChatId { get; set; }
        public ChatModel Chat { get; set; }    

        public int? UserId { get; set; }
        public UserModel User { get; set; }

        public ChatUserModel() { }
    }
}