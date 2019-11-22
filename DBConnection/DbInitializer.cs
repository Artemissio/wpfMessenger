using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Models;
using WpfMessenger.Repositories;

namespace WpfMessenger.DBConnection
{
    public class DbInitializer : CreateDatabaseIfNotExists<MainDataBase>
    {
        UsersRepository _usersRepository;
        ChatsRepository _chatsRepository;
        ChatUserRepository _chatUserRepository;
        MessagesRepository _messagesRepository;

        protected override void Seed(MainDataBase dataBase)
        {
            _usersRepository = new UsersRepository(dataBase);
            _chatsRepository = new ChatsRepository(dataBase);
            _chatUserRepository = new ChatUserRepository(dataBase);
            _messagesRepository = new MessagesRepository(dataBase);
        }
    }
}