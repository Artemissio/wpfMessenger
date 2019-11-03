using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class ChatUser
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public ChatModel Chat { get; set; }
        public UserModel User { get; set; }

        public ChatUser()
        {
            Id = _idGenerator++;
        }
    }
}
