namespace WebShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zzz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_ProductImage", "IsDefault", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_ProductImage", "IsDefault", c => c.String());
        }
    }
}
