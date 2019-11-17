using System;
using System.ComponentModel;
using System.Windows;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class RegistrationViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        string _name;
        string _surname;
        string _number;
        string _nickname;
        string _password;
        string _confirmPassword;

        private RelayCommand _signUp;
        public event EventHandler Closing;

        public bool Validated = false;

        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
                    case "Name":
                        if (!DataValidation.ValidateString(Name))
                            message = "NAME should be only letters and 3-15 symbols";
                        break;

                    case "Surname":
                        if (!DataValidation.ValidateString(Surname))
                            message = "SURNAME should be only letters and 3-15 symbols";
                        break;

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

                    case "ConfirmPassword":
                        if (!DataValidation.ValidateConfirmation(Password, ConfirmPassword))
                            message =  "Passwords Do Not Match";
                        break;
                }
                Validated = DataValidation.ValidateString(Name) && DataValidation.ValidateString(Surname) &&
                    DataValidation.ValidateString(Nickname) && DataValidation.ValidateNumber(Number) &&
                    DataValidation.ValidatePassword(Password) &&
                    DataValidation.ValidateConfirmation(Password, ConfirmPassword);
                return message;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        #region Fields OnPropertyChanged
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

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

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(ConfirmPassword);
            }
        }
        #endregion

        public string Error
        {
            get {  return this[string.Empty]; }
        }

        public RelayCommand SignUp
        {
            get
            {
                return _signUp ??
                    (_signUp = new RelayCommand(obj => {
                        if (!Validated)
                        {
                            MessageBox.Show("Form Is Invalid or has empty fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        UsersRepository repository = UsersRepository.GetInstance();

                        if (repository.Exists(Nickname, Number))
                        {
                            MessageBox.Show("User already exists");
                            return;
                        }

                        UserModel user = new UserModel()
                        {
                            Name = this.Name,
                            Surname = this.Surname,
                            Nickname = this.Nickname,
                            Password = this.Password,
                            Number = this.Number,
                            IsAdmin = false,
                        };

                        repository.AddUser(user);

                        MessageBox.Show($"Welcome, {user.Nickname} !", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);

                        MainView mainView = new MainView(ref user);
                        mainView.Show();
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}