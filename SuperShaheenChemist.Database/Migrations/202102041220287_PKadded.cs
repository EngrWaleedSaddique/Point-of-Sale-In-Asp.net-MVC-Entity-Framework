namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PKadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderID");
            AlterColumn("dbo.OrderItems", "OrderID", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderItems", "OrderID");
            AddForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders", "Id", cascadeDelete: true);
            DropColumn("dbo.OrderItems", "InvoiceNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "InvoiceNumber", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            AlterColumn("dbo.OrderItems", "OrderID", c => c.Int());
            RenameColumn(table: "dbo.OrderItems", name: "OrderID", newName: "Order_Id");
            CreateIndex("dbo.OrderItems", "Order_Id");
            AddForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
