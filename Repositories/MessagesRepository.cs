using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.DBConnection;
using WpfMessenger.Models;

namespace WpfMessenger.Repositories
{
    public class MessagesRepository : GeneralRepository<MessageModel>
    {
        public MessagesRepository(MainDataBase dataBase) : base(dataBase) { }

        public override void Add(MessageModel entity)
        {
            _database.Entry(entity).State = EntityState.Added;
        }

        public override void Delete(MessageModel entity)
        {
            _database.Entry(entity).State = EntityState.Deleted;
        }

        public override void Edit(MessageModel entity)
        {
            _database.Entry(entity).State = EntityState.Modified;
        }

        public override List<MessageModel> GetAll()
        {
            return _database.Messages.ToList();
        }

        public override List<MessageModel> GetAll(Expression<Func<MessageModel, bool>> predicate)
        {
            return _database.Messages.Where(predicate).ToList();
        }

        public override MessageModel GetById(int id)
        {
            return _database.Messages.Find(id);
        }

        public override void Save()
        {
            _database.SaveChanges();
        }
    }
}
