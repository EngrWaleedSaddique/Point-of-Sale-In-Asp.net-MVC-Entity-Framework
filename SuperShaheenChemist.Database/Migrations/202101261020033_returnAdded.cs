namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class returnAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StockInventries", "Return", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StockInventries", "Return");
        }
    }
}
