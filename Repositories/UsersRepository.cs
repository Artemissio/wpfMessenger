using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.DBConnection;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class UsersRepository : GeneralRepository<UserModel>
    {
        public UsersRepository(MainDataBase database) : base(database) { }

        public override void Add(UserModel entity)
        {
            _database.Entry(entity).State = EntityState.Added;
        }

        public override void Delete(UserModel entity)
        {
            _database.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(UserModel entity)
        {
            _database.Entry(entity).State = EntityState.Modified;
        }

        public override List<UserModel> GetAll()
        {
            return _database.Users.ToList();
        }

        public override List<UserModel> GetAll(Expression<Func<UserModel, bool>> predicate)
        {
            return _database.Users.Where(predicate).ToList();
        }

        public override UserModel GetById(int id)
        {
            return _database.Users.Find(id);
        }

        public override void Save()
        {
            _database.SaveChanges();
        }
    }
}