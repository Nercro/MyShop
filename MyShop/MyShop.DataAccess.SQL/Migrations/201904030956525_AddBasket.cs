namespace MyShop.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBasket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        basketID = c.String(maxLength: 128),
                        productID = c.String(),
                        quantity = c.Int(nullable: false),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Baskets", t => t.basketID)
                .Index(t => t.basketID);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        createdAt = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "basketID", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "basketID" });
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}
