using System;
using System.ComponentModel;
using System.Windows;
using WpfMessenger.Models;
using WpfMessenger.Repositories;
using WpfMessenger.Validation;
using WpfMessenger.Views;

namespace WpfMessenger.ViewModels
{
    public class LoginViewModel : IDataErrorInfo, INotifyPropertyChanged
    {
        string _number;
        string _password;

        private RelayCommand _openRegistrationPageCommand;
        private RelayCommand _loginCommand;
        private RelayCommand _passwordChangedCommand;

        public bool Validated = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Closing;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public string this[string columnName]
        {
            get
            {
                string message = string.Empty;

                switch (columnName)
                {
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

                Validated = DataValidation.ValidateNumber(Number) && DataValidation.ValidatePassword(Password);

                return message;
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

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Error
        {
            get { return this[string.Empty]; }
        }

        public RelayCommand OpenRegistrationPageCommand
        {
            get
            {
                return _openRegistrationPageCommand ??
                    (_openRegistrationPageCommand = new RelayCommand(obj =>
                    {
                        RegistrationView registration = new RegistrationView();
                        registration.Show();
                    }));
            }
        }

        public RelayCommand PasswordChangedCommand
        {
            get
            {
                return _passwordChangedCommand ??
                    (_passwordChangedCommand = new RelayCommand(obj =>
                    {
                        RestorePasswordView restorePasswordView = new RestorePasswordView();
                        restorePasswordView.Show();
                    }));
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = new RelayCommand(obj =>
                    {
                        if (!Validated)
                        {
                            MessageBox.Show("Form Is Invalid or has empty fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        UsersRepository repository = UsersRepository.GetInstance();

                        UserModel user = repository.GetUser(Number, Password);

                        if (user == null)
                        {
                            MessageBox.Show("Number or Password Are Incorrect");
                            return;
                        }

                        MessageBox.Show($"Welcome, {user.Nickname} !", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);

                        MainView mainView = new MainView(ref user);
                        mainView.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
    }
}