using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfMessenger.Models;
using System.Threading.Tasks;

namespace WpfMessenger.Interfaces
{
    interface IDialog : IListenable, IMessages
    {
        void Send(string message, UserModel recipient);
    }
}