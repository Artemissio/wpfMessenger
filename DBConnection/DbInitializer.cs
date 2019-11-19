using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMessenger.Repositories;

namespace WpfMessenger.DBConnection
{
    public class DbInitializer : CreateDatabaseIfNotExists<MainDataBase>
    {
        UsersRepository _usersRepository;
        ChatsRepository _chatsRepository;
        ChatUserRepository _chatUserRepository;
        MessagesRepository _messagesRepository;
    }
}
