namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReturnPurchaseTableAdded1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnPurchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        TotalAmount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReturnPurchases", "ProductId", "dbo.Products");
            DropIndex("dbo.ReturnPurchases", new[] { "ProductId" });
            DropTable("dbo.ReturnPurchases");
        }
    }
}
