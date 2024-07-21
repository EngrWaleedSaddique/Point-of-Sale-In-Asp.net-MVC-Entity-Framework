namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cashiers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        MobileNumber = c.String(maxLength: 13),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configs",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Distributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        BatchNo = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(nullable: false),
                        Discount = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Loose = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CustomerName = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        GenericName = c.String(),
                        BatchNo = c.String(),
                        BarCode = c.String(),
                        Description = c.String(),
                        MedicineTypeId = c.Int(nullable: false),
                        ImageURL = c.String(),
                        CustDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CompanyId = c.Int(nullable: false),
                        DistributorId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        MinStock = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SideEffects = c.String(),
                        PackSize = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Location = c.String(),
                        ManufactureDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        UnitCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitRetail = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PackRetailCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SalesTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                        TransactionId = c.Int(nullable: false),
                        BatchNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchaseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseOrderNo = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.StockInventries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Received = c.Int(nullable: false),
                        Sale = c.Int(nullable: false),
                        Stock = c.Int(nullable: false),
                        LooseSale = c.Int(nullable: false),
                        Return = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalAmount = c.Int(nullable: false),
                        BatchNo = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.PurchaseOrderMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        totalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PurchaseProductsMasters",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            CreateTable(
                "dbo.ReturnPurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        BatchNo = c.String(),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ReturnSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        BatchNo = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(nullable: false),
                        Discount = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Loose = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.SaleProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.MedicineTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnSales", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ReturnSales", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.ReturnPurchases", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.StockInventries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseOrders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PurchaseProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderItems", "OrderID", "dbo.Orders");
            DropIndex("dbo.ReturnSales", new[] { "ProductID" });
            DropIndex("dbo.ReturnSales", new[] { "OrderID" });
            DropIndex("dbo.ReturnPurchases", new[] { "ProductId" });
            DropIndex("dbo.StockInventries", new[] { "ProductId" });
            DropIndex("dbo.PurchaseOrders", new[] { "ProductId" });
            DropIndex("dbo.PurchaseProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderItems", new[] { "ProductID" });
            DropIndex("dbo.OrderItems", new[] { "OrderID" });
            DropTable("dbo.MedicineTypes");
            DropTable("dbo.SaleProducts");
            DropTable("dbo.ReturnSales");
            DropTable("dbo.ReturnPurchases");
            DropTable("dbo.PurchaseProductsMasters");
            DropTable("dbo.PurchaseOrderMasters");
            DropTable("dbo.StockInventries");
            DropTable("dbo.PurchaseOrders");
            DropTable("dbo.PurchaseProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Distributors");
            DropTable("dbo.Configs");
            DropTable("dbo.Companies");
            DropTable("dbo.Categories");
            DropTable("dbo.Cashiers");
        }
    }
}
