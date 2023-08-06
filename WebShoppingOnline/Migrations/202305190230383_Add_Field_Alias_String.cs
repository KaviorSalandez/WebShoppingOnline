namespace WebShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Field_Alias_String : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Category", "Alias", c => c.String());
            AddColumn("dbo.tb_New", "Alias", c => c.String());
            AddColumn("dbo.tb_Post", "Alias", c => c.String());
            AddColumn("dbo.tb_Product", "Alias", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "Alias");
            DropColumn("dbo.tb_Post", "Alias");
            DropColumn("dbo.tb_New", "Alias");
            DropColumn("dbo.tb_Category", "Alias");
        }
    }
}
