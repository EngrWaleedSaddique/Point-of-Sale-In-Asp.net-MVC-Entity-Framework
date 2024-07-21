namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transactionIdAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseProducts", "TransactionId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseProducts", "TransactionId");
        }
    }
}
