namespace Quartett.Web.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class DatabaseGeneratedKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Characteristics", "Card_Id", "dbo.Cards");
            DropForeignKey("dbo.PlayerCards", "CardId", "dbo.Cards");
            DropForeignKey("dbo.Characteristics", "TypeId", "dbo.CharacteristicTypes");
            DropForeignKey("dbo.PlayerCards", "Game_Id", "dbo.Games");
            DropPrimaryKey("dbo.Cards");
            DropPrimaryKey("dbo.Characteristics");
            DropPrimaryKey("dbo.CharacteristicTypes");
            DropPrimaryKey("dbo.Games");
            DropPrimaryKey("dbo.PlayerCards");
            AlterColumn("dbo.Cards", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.Characteristics", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.CharacteristicTypes", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.Games", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AlterColumn("dbo.PlayerCards", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"));
            AddPrimaryKey("dbo.Cards", "Id");
            AddPrimaryKey("dbo.Characteristics", "Id");
            AddPrimaryKey("dbo.CharacteristicTypes", "Id");
            AddPrimaryKey("dbo.Games", "Id");
            AddPrimaryKey("dbo.PlayerCards", "Id");
            AddForeignKey("dbo.Characteristics", "Card_Id", "dbo.Cards", "Id");
            AddForeignKey("dbo.PlayerCards", "CardId", "dbo.Cards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Characteristics", "TypeId", "dbo.CharacteristicTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlayerCards", "Game_Id", "dbo.Games", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerCards", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Characteristics", "TypeId", "dbo.CharacteristicTypes");
            DropForeignKey("dbo.PlayerCards", "CardId", "dbo.Cards");
            DropForeignKey("dbo.Characteristics", "Card_Id", "dbo.Cards");
            DropPrimaryKey("dbo.PlayerCards");
            DropPrimaryKey("dbo.Games");
            DropPrimaryKey("dbo.CharacteristicTypes");
            DropPrimaryKey("dbo.Characteristics");
            DropPrimaryKey("dbo.Cards");
            AlterColumn("dbo.PlayerCards", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Games", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.CharacteristicTypes", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Characteristics", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Cards", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.PlayerCards", "Id");
            AddPrimaryKey("dbo.Games", "Id");
            AddPrimaryKey("dbo.CharacteristicTypes", "Id");
            AddPrimaryKey("dbo.Characteristics", "Id");
            AddPrimaryKey("dbo.Cards", "Id");
            AddForeignKey("dbo.PlayerCards", "Game_Id", "dbo.Games", "Id");
            AddForeignKey("dbo.Characteristics", "TypeId", "dbo.CharacteristicTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlayerCards", "CardId", "dbo.Cards", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Characteristics", "Card_Id", "dbo.Cards", "Id");
        }
    }
}
