using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Interfaces;
using WpfMessenger.Models;

namespace WpfMessenger.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        IUser _user;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get { return _user.Name; }
            set
            {
                if(_user.Name != value)
                {
                    _user.Name = value;
                    OnPropertyChange("Name");
                }
            }
        }
        public string Surname
        {
            get { return _user.Surname; }
            set
            {
                if (_user.Surname != value)
                {
                    _user.Surname = value;
                    OnPropertyChange("Surname");
                }
            }
        }

        public string FullName
        {
            get { return Name + " " + Surname; }
        }
    }
}