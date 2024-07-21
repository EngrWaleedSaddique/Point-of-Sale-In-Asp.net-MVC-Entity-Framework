namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productsRemovedCompaniesTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Products", new[] { "CompanyId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Products", "CompanyId");
            AddForeignKey("dbo.Products", "CompanyId", "dbo.Companies", "Id", cascadeDelete: true);
        }
    }
}
