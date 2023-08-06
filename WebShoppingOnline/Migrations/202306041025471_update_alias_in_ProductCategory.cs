namespace WebShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_alias_in_ProductCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_ProductCategory", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_ProductCategory", "Alias");
        }
    }
}
