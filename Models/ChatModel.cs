using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfMessenger.Models
{
    public class ChatModel
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int AdminID { get; set; }
        public UserModel Admin { get; set; }

        public ChatModel() { }
    }
}
