using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Validation;

namespace WpfMessenger.Models
{
    public class UserModel 
    {
        public virtual int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        
        public string Nickname { get; set; }
        public string Number { get; set; }
        public string Password { get; set; }

        public string Fullname
        {
            get { return Name + " " + Surname; }
        }
        public virtual List<ChatUserModel> ChatUsers { get; set; }

        public UserModel() { }
    }
}
