using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.ViewModels
{
    public class MessageViewModel
    {
        public string Fullname { get; set; }
        public string Text { get; set; }
        public DateTime Sent { get; set; }
    }
}
