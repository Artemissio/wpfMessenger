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
    public class ChatsRepository : GeneralRepository<ChatModel>
    {
        public ChatsRepository(MainDataBase dataBase) : base(dataBase) { }

        public override void Add(ChatModel entity)
        {
            _database.Entry(entity).State = EntityState.Added;
        }

        public override void Delete(ChatModel entity)
        {
            _database.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(ChatModel entity)
        {
            _database.Entry(entity).State = EntityState.Modified;
        }

        public override List<ChatModel> GetAll()
        {
            return _database.Chats.ToList();
        }

        public override List<ChatModel> GetAll(Expression<Func<ChatModel, bool>> predicate)
        {
            return _database.Chats.Where(predicate).ToList();
        }
        public override ChatModel GetById(int id)
        {
            return _database.Chats.Find(id);
        }

        public ChatModel GetById(int? id)
        {
            return _database.Chats.Find(id);
        }

        public override void Save()
        {
            _database.SaveChanges();
        }
    }
}