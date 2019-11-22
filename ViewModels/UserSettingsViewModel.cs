using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using WpfMessenger.DBConnection;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class UserSettingsViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        UserModel user;

        RelayCommand _save;
        RelayCommand _back;

        public event EventHandler Closing;

        static string _previousNickname;
        static string _previousNumber;

        string _number;
        string _nickname;
        string _password;
        public bool Validated = false;

        public UserSettingsViewModel(UserModel user)
        {
            this.user = user;

            _nickname = this.user.Nickname;
            _number = this.user.Number;
            _password = this.user.Password;

            _previousNickname = _nickname;
            _previousNumber = _number;
        }

        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
                    case "Nickname":
                        if (!DataValidation.ValidateString(Nickname))
                            message = "NICKNAME should be only letters and 3-15 symbols";
                        break;

                    case "Number":
                        if (!DataValidation.ValidateNumber(Number))
                            message = "NUMBER should be XXX-XXX-XXXX format";
                        break;

                    case "Password":
                        if (!DataValidation.ValidatePassword(Password))
                            message = "Password should be 3-15 symbols, " +
                                "uppercase and lowercase letter and number";
                        break;
                }
                Validated = DataValidation.ValidateString(Nickname) && DataValidation.ValidateNumber(Number) &&
                    DataValidation.ValidatePassword(Password);
                return message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region Fields OnPropertyChanged
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged(nameof(Number));
            }
        }
        public string Nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value.ToLower();
                OnPropertyChanged(nameof(Nickname));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        #endregion

        public string Error
        {
            get { return this[string.Empty]; }
        }

        public RelayCommand Save
        {
            get
            {
                return _save ??
                    (_save = new RelayCommand(o =>
                    {
                        if (!Validated)
                        {
                            MessageBox.Show("Form Is Invalid or has empty fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        using(MainDataBase dataBase = new MainDataBase())
                        {
                            UsersRepository repository = new UsersRepository(dataBase);

                            if(_previousNickname != Nickname)
                            {
                                var user = repository.GetAll(u => u.Nickname == Nickname).FirstOrDefault();

                                if (user != null)
                                {
                                    MessageBox.Show("User already exists");
                                    return;
                                }
                            }

                            if (_previousNumber != Number)
                            {
                                var user = repository.GetAll(u => u.Number == Number).FirstOrDefault();

                                if (user != null)
                                {
                                    MessageBox.Show("User already exists");
                                    return;
                                }
                            }

                            if (_previousNickname != _nickname && _previousNumber != _number)
                            {
                                var user = repository.GetAll(u => u.Nickname == Nickname || u.Number == Number).FirstOrDefault();

                                if (user != null)
                                {
                                    MessageBox.Show("User already exists");
                                    return;
                                }
                            }

                            user.Nickname = Nickname;
                            user.Number = Number;
                            user.Password = Password;

                            repository.Edit(user);
                            dataBase.SaveChanges();
                        }

                        MessageBox.Show("Data Is Successfully Changed", "Ok", MessageBoxButton.OK, MessageBoxImage.Information);

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand Back
        {
            get
            {
                return _back ??
                    (_back = new RelayCommand(obj =>
                    {
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}