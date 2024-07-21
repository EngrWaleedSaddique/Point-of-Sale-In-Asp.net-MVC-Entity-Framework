namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BatchNoAddedInReturnTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnPurchases", "BatchNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReturnPurchases", "BatchNo");
        }
    }
}
