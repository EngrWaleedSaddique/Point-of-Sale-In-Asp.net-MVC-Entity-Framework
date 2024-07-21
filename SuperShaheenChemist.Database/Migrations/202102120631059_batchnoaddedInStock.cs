namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class batchnoaddedInStock : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockInventries", "BatchNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockInventries", "BatchNo");
        }
    }
}
