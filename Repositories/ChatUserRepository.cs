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
    public class ChatUserRepository : GeneralRepository<ChatUserModel>
    {
        public ChatUserRepository(MainDataBase dataBase) : base(dataBase) { }

        public override void Add(ChatUserModel entity)
        {
            _database.Entry(entity).State = EntityState.Added;
        }

        public override void Delete(ChatUserModel entity)
        {
            _database.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(ChatUserModel entity)
        {
            _database.Entry(entity).State = EntityState.Modified;
        }

        public override List<ChatUserModel> GetAll()
        {
            return _database.ChatUsers.ToList();
        }

        public override List<ChatUserModel> GetAll(Expression<Func<ChatUserModel, bool>> predicate)
        {
            return _database.ChatUsers.Where(predicate).ToList();
        }

        public override ChatUserModel GetById(int id)
        {
            return _database.ChatUsers.Find(id);
        }

        public override void Save()
        {
            _database.SaveChanges();
        }
    }
}