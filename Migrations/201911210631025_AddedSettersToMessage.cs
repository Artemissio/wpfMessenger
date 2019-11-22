namespace WpfMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSettersToMessage : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.MessageModels", name: "ChatModel_Id", newName: "Chat_Id");
            RenameIndex(table: "dbo.MessageModels", name: "IX_ChatModel_Id", newName: "IX_Chat_Id");
            AddColumn("dbo.MessageModels", "Text", c => c.String());
            AddColumn("dbo.MessageModels", "Sent", c => c.DateTime(nullable: false));
            AddColumn("dbo.MessageModels", "Sender_Id", c => c.Int());
            CreateIndex("dbo.MessageModels", "Sender_Id");
            AddForeignKey("dbo.MessageModels", "Sender_Id", "dbo.UserModels", "Id");
            DropColumn("dbo.MessageModels", "SenderID");
            DropColumn("dbo.MessageModels", "ChatID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MessageModels", "ChatID", c => c.Int(nullable: false));
            AddColumn("dbo.MessageModels", "SenderID", c => c.Int(nullable: false));
            DropForeignKey("dbo.MessageModels", "Sender_Id", "dbo.UserModels");
            DropIndex("dbo.MessageModels", new[] { "Sender_Id" });
            DropColumn("dbo.MessageModels", "Sender_Id");
            DropColumn("dbo.MessageModels", "Sent");
            DropColumn("dbo.MessageModels", "Text");
            RenameIndex(table: "dbo.MessageModels", name: "IX_Chat_Id", newName: "IX_ChatModel_Id");
            RenameColumn(table: "dbo.MessageModels", name: "Chat_Id", newName: "ChatModel_Id");
        }
    }
}
