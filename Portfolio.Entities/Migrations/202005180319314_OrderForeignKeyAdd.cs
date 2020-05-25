namespace Portfolio.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderForeignKeyAdd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "OrderDetailId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrderDetailId", c => c.Long(nullable: false));
        }
    }
}
