namespace WebShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class themRequireVaoThuocTInh1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Post", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Post", "Title", c => c.String());
        }
    }
}
