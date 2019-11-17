using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.DBConnection
{
    public class MainDataBase : DbContext 
    {
        static MainDataBase()
        {
            Database.SetInitializer<MainDataBase>(new DbInitializer());
        }

    }
}
