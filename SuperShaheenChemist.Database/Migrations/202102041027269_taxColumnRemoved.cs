namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taxColumnRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderItems", "Tax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Tax", c => c.Int(nullable: false));
        }
    }
}
