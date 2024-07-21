namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalAmountColumnRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseOrders", "TotalAmountOfOrder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseOrders", "TotalAmountOfOrder", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
