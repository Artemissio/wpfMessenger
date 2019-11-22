using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.ViewModels
{
    public class UserProfileViewModel
    {
        UserModel user;

        //RelayCommand _sendMessage;

        public UserProfileViewModel(UserModel user)
        {
            this.user = user;
        }

        public string Fullname { get { return user.Fullname; } }
        public string Number { get { return user.Number; } }
        public string Nickname { get { return user.Nickname; } }
    }
}
