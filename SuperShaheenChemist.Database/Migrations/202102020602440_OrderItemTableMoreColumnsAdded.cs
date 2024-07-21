namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderItemTableMoreColumnsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "BatchNo", c => c.String());
            AddColumn("dbo.OrderItems", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrderItems", "ExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderItems", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "Tax", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItems", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItems", "Amount");
            DropColumn("dbo.OrderItems", "Tax");
            DropColumn("dbo.OrderItems", "Discount");
            DropColumn("dbo.OrderItems", "ExpiryDate");
            DropColumn("dbo.OrderItems", "Price");
            DropColumn("dbo.OrderItems", "BatchNo");
        }
    }
}
