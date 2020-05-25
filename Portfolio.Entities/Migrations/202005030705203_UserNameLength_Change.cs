namespace Portfolio.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameLength_Change : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Inquiries", "UserName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Reviews", "UserName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "UserName", c => c.String(maxLength: 10));
            AlterColumn("dbo.Inquiries", "UserName", c => c.String(maxLength: 10));
        }
    }
}
