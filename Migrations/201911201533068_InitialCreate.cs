namespace WpfMessenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AdminID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserModels", t => t.AdminID, cascadeDelete: true)
                .Index(t => t.AdminID);
            
            CreateTable(
                "dbo.UserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Nickname = c.String(),
                        Number = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatUserModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(),
                        UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatModels", t => t.ChatId)
                .ForeignKey("dbo.UserModels", t => t.UserId)
                .Index(t => t.ChatId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MessageModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderID = c.Int(nullable: false),
                        ChatID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChatUserModels", "UserId", "dbo.UserModels");
            DropForeignKey("dbo.ChatUserModels", "ChatId", "dbo.ChatModels");
            DropForeignKey("dbo.ChatModels", "AdminID", "dbo.UserModels");
            DropIndex("dbo.ChatUserModels", new[] { "UserId" });
            DropIndex("dbo.ChatUserModels", new[] { "ChatId" });
            DropIndex("dbo.ChatModels", new[] { "AdminID" });
            DropTable("dbo.MessageModels");
            DropTable("dbo.ChatUserModels");
            DropTable("dbo.UserModels");
            DropTable("dbo.ChatModels");
        }
    }
}
