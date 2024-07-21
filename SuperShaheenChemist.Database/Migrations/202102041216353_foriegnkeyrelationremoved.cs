namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foriegnkeyrelationremoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            RenameColumn(table: "dbo.OrderItems", name: "OrderID", newName: "Order_Id");
            AddColumn("dbo.OrderItems", "InvoiceNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderItems", "Order_Id", c => c.Int());
            CreateIndex("dbo.OrderItems", "Order_Id");
            AddForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderItems", new[] { "Order_Id" });
            AlterColumn("dbo.OrderItems", "Order_Id", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "InvoiceNumber");
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderID");
            CreateIndex("dbo.OrderItems", "OrderID");
            AddForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}
