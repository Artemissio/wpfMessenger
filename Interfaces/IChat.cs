using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfMessenger.Models;
using System.Threading.Tasks;

namespace WpfMessenger.Interfaces
{
    interface IChat : IListenable, IMessages
    {
        List<UserModel> Members { get; }
        void Send(string message);
    }
}