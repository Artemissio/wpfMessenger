using System;
using System.Collections.Generic;
using System.Linq;
using WpfMessenger.Interfaces;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.Models
{
    public class DialogModel /*: IDialog*/
    {
        public List<MessageModel> Messages { get; }
        public DialogModel()
        {
            Messages = new List<MessageModel>();
        }

        public void Send(string message, UserModel recipient)
        {
            throw new NotImplementedException();
        }

        public void Listen()
        {
            throw new NotImplementedException();
        }
    }
}
