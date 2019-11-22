namespace WpfMessenger.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WpfMessenger.DBConnection.MainDataBase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "WpfMessenger.DBConnection.MainDataBase";
        }

        protected override void Seed(WpfMessenger.DBConnection.MainDataBase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
