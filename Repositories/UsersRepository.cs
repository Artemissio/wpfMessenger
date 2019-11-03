using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class UsersRepository
    {
        static UsersRepository _repository;

        UserModel _user;
        List<UserModel> _users;
        UsersRepository()
        {
            _users = new List<UserModel>() {
                new UserModel("Artem", "Tarasenko", "11111", "Artemissio", "artemissio", false),
                new UserModel("John", "Snow", "22222", "JSnow", "jsnow",false),
                new UserModel("Steve", "Jobes", "33333", "SteveJ", "steve", false)
            };
        }

        public static UsersRepository GetInstance()
        {
            if (_repository == null)
                _repository = new UsersRepository();
            return _repository;
        }

        public UserModel GetUser(string number, string password)
        {
            for(int i=0; i<_users.Count; i++)
            {
                if (_users[i].Number == number  && _users[i].Password == password)
                    _user = _users[i];
            }
            return _user;
        }

        public void AddUser(UserModel user)
        {
            if(user != null)
                _users.Add(user);
        }

        public List<UserModel> GetUsers()
        {
            return _users;
        }
    }
}