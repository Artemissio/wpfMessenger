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

        public virtual List<MessageModel> Messages { get; set; }
        public virtual List<ChatUserModel> ChatUsers { get; set; }

        public ChatModel() { }
    }
}
