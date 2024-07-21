namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductRemovedFromTypes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Products", "DistributorId", "dbo.Distributors");
            DropForeignKey("dbo.Products", "MedicineTypeId", "dbo.MedicineTypes");
            DropIndex("dbo.Products", new[] { "MedicineTypeId" });
            DropIndex("dbo.Products", new[] { "DistributorId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Products", "CategoryId");
            CreateIndex("dbo.Products", "DistributorId");
            CreateIndex("dbo.Products", "MedicineTypeId");
            AddForeignKey("dbo.Products", "MedicineTypeId", "dbo.MedicineTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "DistributorId", "dbo.Distributors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
