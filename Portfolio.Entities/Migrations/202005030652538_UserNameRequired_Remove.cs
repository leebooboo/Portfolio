namespace Portfolio.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameRequired_Remove : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inquiries", "UserName", c => c.String(maxLength: 10));
            AlterColumn("dbo.Reviews", "UserName", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "UserName", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Inquiries", "UserName", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
