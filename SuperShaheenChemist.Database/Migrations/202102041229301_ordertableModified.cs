namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ordertableModified : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "ProductId");
            DropColumn("dbo.Orders", "Qty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Qty", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
        }
    }
}
