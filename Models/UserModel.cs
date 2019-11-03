using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Interfaces;

namespace WpfMessenger.Models
{
    public class UserModel /*: IUser*/
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public List<DeviceModel> Devices { get; }
        public List<ChatModel> Chats { get; }
        //public List<UserModel> Contacts { get; }

        public UserModel(string name, string number)
        {
            Name = name;
            Number = number;
        }
        public UserModel(string name, string surname, string number, string nickname, string password, bool admin)
                        :this(name, number)
        {
            Surname = surname;
            Nickname = nickname;
            Password = password;
            IsAdmin = admin;

            Devices = new List<DeviceModel>();
            Chats = new List<ChatModel>();
            //Contacts = new List<UserModel>();
        }

        public void AddDevice(DeviceModel device)
        {
            if (!Devices.Contains(device))
                Devices.Add(device);
        }

        public void AddChat(ChatModel chat)
        {
            Chats.Add(chat);
        }

        //public void AddContact(UserModel contact)
        //{
        //    if(!Contacts.Contains(contact))
        //        Contacts.Add(contact);
        //}

        public void RemoveMember(ChatModel chat, UserModel user)
        {
            if (IsAdmin)
            {
                if (chat.Members.Contains(user))
                    chat.Members.Remove(user);
            }      
        }
    }
}
