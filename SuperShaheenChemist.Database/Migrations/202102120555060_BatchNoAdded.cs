namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BatchNoAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseProducts", "BatchNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseProducts", "BatchNo");
        }
    }
}
