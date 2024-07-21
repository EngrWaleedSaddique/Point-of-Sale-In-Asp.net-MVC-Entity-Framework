namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableNamehanged : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.OrderItems", newName: "SaleItems");
            AddColumn("dbo.SaleItems", "Loose", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SaleItems", "Loose");
            RenameTable(name: "dbo.SaleItems", newName: "OrderItems");
        }
    }
}
