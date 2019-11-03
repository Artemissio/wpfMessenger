using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Interfaces
{
    interface IMessages
    {
        List<MessageModel> Messages { get; }
    }
}