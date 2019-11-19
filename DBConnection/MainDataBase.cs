using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;

namespace WpfMessenger.DBConnection
{
    public class MainDataBase : DbContext 
    {
        static MainDataBase()
        {
            Database.SetInitializer(new DbInitializer());
        }
        public MainDataBase() : base("DbConnection") { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<ChatUserModel> ChatUsers { get; set; }
        public DbSet<MessageModel> Messages { get; set; }

    }
}
