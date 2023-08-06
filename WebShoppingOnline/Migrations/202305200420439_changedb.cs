namespace WebShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_New", "Title", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_New", "Title", c => c.String());
        }
    }
}
