using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.DBConnection;

namespace WpfMessenger.Repositories
{
    public abstract class GeneralRepository<T>  : IRepository<T> where T : class
    {
        protected MainDataBase _database;

        public GeneralRepository(MainDataBase dataBase)
        {
            _database = dataBase;
        }

        public abstract T GetById(int id);
        public abstract List<T> GetAll();
        public abstract List<T> GetAll(Expression<Func<T, bool>> predicate);
        public abstract void Add(T entity);
        public abstract void Delete(T entity);
        public abstract void Edit(T entity);
        public abstract void Save();
    }
}
