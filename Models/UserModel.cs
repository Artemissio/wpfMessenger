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
        private static int _idGenerator = 0;
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nickname { get; set; }
        public string Number { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public string Fullname
        {
            get { return Name + " " + Surname; }
        }

        public UserModel() { }

        public UserModel(string name, string number)
        {
            Id = _idGenerator++;
            Name = name;
            Number = number;
        }
        public UserModel(string name, string surname, string number, string nickname, string password, bool admin)
        {
            Id = _idGenerator++;
            Name = name;
            Surname = surname;
            Nickname = nickname;
            Number = number;
            Password = password;
            IsAdmin = admin;
        }
    }
}
