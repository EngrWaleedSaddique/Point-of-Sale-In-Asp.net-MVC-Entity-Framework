namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TAbleFieldsChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnSales", "OrderID", c => c.Int(nullable: false));
            AddColumn("dbo.ReturnSales", "BatchNo", c => c.String());
            AddColumn("dbo.ReturnSales", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ReturnSales", "ExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReturnSales", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.ReturnSales", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.ReturnSales", "Loose", c => c.Int(nullable: false));
            AddColumn("dbo.ReturnSales", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ReturnSales", "ProductID", c => c.Int(nullable: false));
            CreateIndex("dbo.ReturnSales", "OrderID");
            CreateIndex("dbo.ReturnSales", "ProductID");
            AddForeignKey("dbo.ReturnSales", "OrderID", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ReturnSales", "ProductID", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.ReturnSales", "name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReturnSales", "name", c => c.String());
            DropForeignKey("dbo.ReturnSales", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ReturnSales", "OrderID", "dbo.Orders");
            DropIndex("dbo.ReturnSales", new[] { "ProductID" });
            DropIndex("dbo.ReturnSales", new[] { "OrderID" });
            DropColumn("dbo.ReturnSales", "ProductID");
            DropColumn("dbo.ReturnSales", "Amount");
            DropColumn("dbo.ReturnSales", "Loose");
            DropColumn("dbo.ReturnSales", "Quantity");
            DropColumn("dbo.ReturnSales", "Discount");
            DropColumn("dbo.ReturnSales", "ExpiryDate");
            DropColumn("dbo.ReturnSales", "Price");
            DropColumn("dbo.ReturnSales", "BatchNo");
            DropColumn("dbo.ReturnSales", "OrderID");
        }
    }
}
