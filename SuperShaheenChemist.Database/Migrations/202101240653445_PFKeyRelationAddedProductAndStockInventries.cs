namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PFKeyRelationAddedProductAndStockInventries : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.StockInventries", "ProductId");
            AddForeignKey("dbo.StockInventries", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockInventries", "ProductId", "dbo.Products");
            DropIndex("dbo.StockInventries", new[] { "ProductId" });
        }
    }
}
