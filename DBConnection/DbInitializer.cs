using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMessenger.DBConnection
{
    public class DbInitializer : CreateDatabaseIfNotExists<MainDataBase>
    {

    }
}
