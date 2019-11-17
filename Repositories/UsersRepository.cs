using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class UsersRepository
    {
        static UsersRepository _repository;

        ObservableCollection<UserModel> _users;

        UsersRepository()
        {
            _users = new ObservableCollection<UserModel>();

            _users = new ObservableCollection<UserModel>() {
                new UserModel("Artem", "Tarasenko", "098-078-9920", "artemissio", "Password1", false),
                new UserModel("Max", "Terentiev", "000-000-0000", "maxter", "Password1", false),
                new UserModel("Max", "Yaremenko", "000-000-0001", "rabotodatel", "Password1", false),
                new UserModel("Misha", "Dragan", "000-000-0002", "basically_unknown", "Password1", false),
                new UserModel("Yulia", "Koval", "000-000-0003", "yulkvl", "Password1", false),
                new UserModel("Yana", "Koval", "000-000-0004", "ynkvl", "Password1", false),
            };
        }

        public static UsersRepository GetInstance()
        {
            if (_repository == null)
                _repository = new UsersRepository();
            return _repository;
        }
        public UserModel GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public UserModel GetUser(string number, string password)
        {
            return _users.FirstOrDefault(u => u.Number == number && u.Password == password);
        }

        public bool Exists(string nickname, string number)
        {
            return _users.Any(u => u.Nickname == nickname || u.Number == number);
        }

        public void AddUser(UserModel user)
        {
            if (user != null)
                _users.Add(user);
        }

        public void UpdateUser(int id, string nickname, string number, string password)
        {
            UserModel user = _users.FirstOrDefault(u => u.Id == id);

            user.Nickname = nickname;
            user.Number = number;
            user.Password = password;
        }

        public void RemoveUser(UserModel user)
        {
            if (user != null)
                _users.Remove(user);
        }

        public ObservableCollection<UserModel> GetUsers()
        {
            return _users;
        }

        public IEnumerable<UserModel> GetUsers(string value, UserModel user)
        {
            if (string.IsNullOrEmpty(value))
                return _users.ToList().Where(u => u != user);

            return _users.ToList().Where(u => u != user &&
                                      (u.Nickname.Contains(value)
                                        || u.Name.Contains(value) 
                                        || u.Surname.Contains(value)));
        }
    }
}