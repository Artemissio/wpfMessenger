namespace WpfMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSetters : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ChatUserModels", name: "ChatId", newName: "Chat_Id");
            RenameColumn(table: "dbo.ChatUserModels", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.ChatUserModels", name: "IX_ChatId", newName: "IX_Chat_Id");
            RenameIndex(table: "dbo.ChatUserModels", name: "IX_UserId", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ChatUserModels", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.ChatUserModels", name: "IX_Chat_Id", newName: "IX_ChatId");
            RenameColumn(table: "dbo.ChatUserModels", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.ChatUserModels", name: "Chat_Id", newName: "ChatId");
        }
    }
}
