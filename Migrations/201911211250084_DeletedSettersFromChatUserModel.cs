namespace WpfMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedSettersFromChatUserModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ChatUserModels", "ChatId", "dbo.ChatModels");
            DropForeignKey("dbo.ChatUserModels", "UserId", "dbo.UserModels");
            DropIndex("dbo.ChatUserModels", new[] { "ChatId" });
            DropIndex("dbo.ChatUserModels", new[] { "UserId" });
            AlterColumn("dbo.ChatUserModels", "ChatId", c => c.Int(nullable: false));
            AlterColumn("dbo.ChatUserModels", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ChatUserModels", "ChatId");
            CreateIndex("dbo.ChatUserModels", "UserId");
            AddForeignKey("dbo.ChatUserModels", "ChatId", "dbo.ChatModels", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ChatUserModels", "UserId", "dbo.UserModels", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatUserModels", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.ChatUserModels", "ChatId", "dbo.ChatModels");
            DropIndex("dbo.ChatUserModels", new[] { "UserId" });
            DropIndex("dbo.ChatUserModels", new[] { "ChatId" });
            AlterColumn("dbo.ChatUserModels", "UserId", c => c.Int());
            AlterColumn("dbo.ChatUserModels", "ChatId", c => c.Int());
            CreateIndex("dbo.ChatUserModels", "UserId");
            CreateIndex("dbo.ChatUserModels", "ChatId");
            AddForeignKey("dbo.ChatUserModels", "UserId", "dbo.UserModels", "Id");
            AddForeignKey("dbo.ChatUserModels", "ChatId", "dbo.ChatModels", "Id");
        }
    }
}
