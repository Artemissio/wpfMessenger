using System.Data.Entity;
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
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<ChatUserModel> ChatUsers { get; set; }
    }
}