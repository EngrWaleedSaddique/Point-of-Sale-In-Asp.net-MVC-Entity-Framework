namespace SuperShaheenChemist.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseOrderMasters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNo = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        totalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PurchaseOrderMasters");
        }
    }
}
