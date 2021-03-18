namespace WebsiteTuDien.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(),
                        Orders = c.Int(),
                        Metakey = c.String(),
                        Metadesc = c.String(),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        Status = c.Int(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        Email = c.String(),
                        Phone = c.Int(nullable: false),
                        Title = c.String(),
                        Detail = c.String(),
                        Flag = c.Int(nullable: false),
                        Reply = c.String(),
                        Status = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Updated_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Link",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(nullable: false),
                        Type = c.String(nullable: false),
                        TableId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(nullable: false),
                        Link = c.String(),
                        TableID = c.Int(),
                        ParentID = c.Int(),
                        Orders = c.Int(nullable: false),
                        Positon = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Updated_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        UserID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        ExportDate = c.DateTime(),
                        DeliveryAddress = c.String(),
                        DeliveryName = c.String(),
                        DeliveryPhone = c.String(),
                        DeliveryEmail = c.String(),
                        Status = c.Int(),
                        Trash = c.Int(),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TopicID = c.Int(nullable: false),
                        Slug = c.String(),
                        Title = c.String(),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                        Type = c.String(),
                        Position = c.String(),
                        MetaKey = c.String(),
                        MetaDesc = c.String(),
                        Status = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Updated_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Slug = c.String(),
                        Img = c.String(),
                        Detail = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Number = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Price_sale = c.Int(nullable: false),
                        Metakey = c.String(),
                        Metadesc = c.String(),
                        Created_at = c.DateTime(),
                        Created_by = c.Int(),
                        Updated_at = c.DateTime(),
                        Updated_by = c.Int(),
                        Status = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                        CatID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        Position = c.String(),
                        Image = c.String(),
                        Orders = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Updated_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        ParentID = c.Int(),
                        Order = c.Int(nullable: false),
                        MetaKey = c.String(),
                        MetaDesc = c.String(),
                        Status = c.Int(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Created_by = c.Int(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        Updated_by = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Topic");
            DropTable("dbo.Slider");
            DropTable("dbo.Product");
            DropTable("dbo.Post");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.Order");
            DropTable("dbo.Menu");
            DropTable("dbo.Link");
            DropTable("dbo.Contact");
            DropTable("dbo.Category");
        }
    }
}
