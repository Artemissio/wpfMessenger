namespace WpfMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageModels", "ChatModel_Id", c => c.Int());
            CreateIndex("dbo.MessageModels", "ChatModel_Id");
            AddForeignKey("dbo.MessageModels", "ChatModel_Id", "dbo.ChatModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MessageModels", "ChatModel_Id", "dbo.ChatModels");
            DropIndex("dbo.MessageModels", new[] { "ChatModel_Id" });
            DropColumn("dbo.MessageModels", "ChatModel_Id");
        }
    }
}
