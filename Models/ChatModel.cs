using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfMessenger.Interfaces;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class ChatModel
    {
        public string Name { get; set; }
        public UserModel Admin { get; set; }  
        public List<UserModel> Members { get; set; }
        public List<MessageModel> Messages { get; }

        public ChatModel(string name, UserModel admin)
        {
            Name = name;
            Admin = admin;

            Members = new List<UserModel>();
            Messages = new List<MessageModel>();
        }
    }
}
