using System;
using System.Collections.Generic;
using System.Linq;
using WpfMessenger.Models;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Interfaces
{
    interface IUser
    {
        string Name { get; set; }
        string Surname { get; set; }
        string Number { get; set; }
        string Nickname { get; set; }
        string Password { get; set; }

        List<DeviceModel> Devices { get; }
        List<ChatModel> Chats { get; }
        List<UserModel> Contacts { get; }

        void AddDevice(DeviceModel device);
        void AddContact(UserModel contact);
        void AddChat(ChatModel chat);
    }
}
