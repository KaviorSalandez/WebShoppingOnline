namespace WebShoppingOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatekey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tb_OrderDetail");
            AddPrimaryKey("dbo.tb_OrderDetail", new[] { "OrderId", "ProductId" });
            DropColumn("dbo.tb_OrderDetail", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_OrderDetail", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.tb_OrderDetail");
            AddPrimaryKey("dbo.tb_OrderDetail", "Id");
        }
    }
}
