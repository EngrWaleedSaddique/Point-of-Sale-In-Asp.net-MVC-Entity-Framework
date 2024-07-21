namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableNamehanged1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SaleItems", newName: "OrderItems");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.OrderItems", newName: "SaleItems");
        }
    }
}
