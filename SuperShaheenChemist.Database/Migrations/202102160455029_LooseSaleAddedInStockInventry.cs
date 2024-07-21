namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LooseSaleAddedInStockInventry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockInventries", "LooseSale", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockInventries", "LooseSale");
        }
    }
}
