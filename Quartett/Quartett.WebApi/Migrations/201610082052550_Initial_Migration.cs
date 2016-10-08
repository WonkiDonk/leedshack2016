namespace Quartett.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Characteristics",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TypeId = c.Guid(nullable: false),
                        Value = c.Double(nullable: false),
                        Card_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CharacteristicTypes", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.Cards", t => t.Card_Id)
                .Index(t => t.TypeId)
                .Index(t => t.Card_Id);
            
            CreateTable(
                "dbo.CharacteristicTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Player1Id = c.String(),
                        Player2Id = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerCards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlayerId = c.String(),
                        CardId = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Game_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cards", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.CardId)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerCards", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlayerCards", "CardId", "dbo.Cards");
            DropForeignKey("dbo.Characteristics", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.Characteristics", "TypeId", "dbo.CharacteristicTypes");
            DropIndex("dbo.PlayerCards", new[] { "Game_Id" });
            DropIndex("dbo.PlayerCards", new[] { "CardId" });
            DropIndex("dbo.Characteristics", new[] { "Card_Id" });
            DropIndex("dbo.Characteristics", new[] { "TypeId" });
            DropTable("dbo.PlayerCards");
            DropTable("dbo.Games");
            DropTable("dbo.CharacteristicTypes");
            DropTable("dbo.Characteristics");
            DropTable("dbo.Cards");
        }
    }
}
