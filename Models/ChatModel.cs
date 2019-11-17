using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfMessenger.Models
{
    public class ChatModel
    {
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public UserModel Admin { get; set; }  
        //public List<UserModel> Members { get; set; }
        public List<MessageModel> Messages { get; }

        public ChatModel(string name, UserModel admin)
        {
            Id = _idGenerator++;
            Name = name;
            Admin = admin;

            //Members = new List<UserModel>();
            Messages = new List<MessageModel>();
        }
    }
}
