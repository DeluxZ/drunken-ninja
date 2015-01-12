namespace Application.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(nullable: false, maxLength: 100),
                        AddressLine2 = c.String(nullable: false, maxLength: 100),
                        Country = c.String(nullable: false, maxLength: 50),
                        State = c.String(nullable: false, maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(nullable: false, maxLength: 15),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.ProfileAddress",
                c => new
                    {
                        ProfileAddressId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        AddressTypeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ProfileAddressId)
                .ForeignKey("dbo.Address", t => t.AddressId)
                .ForeignKey("dbo.AddressType", t => t.AddressTypeId)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.ProfileId)
                .Index(t => t.AddressId)
                .Index(t => t.AddressTypeId);
            
            CreateTable(
                "dbo.AddressType",
                c => new
                    {
                        AddressTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AddressTypeId);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        ProfileId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ProfileId);
            
            CreateTable(
                "dbo.ProfilePhone",
                c => new
                    {
                        ProfilePhoneId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(nullable: false),
                        PhoneId = c.Int(nullable: false),
                        PhoneTypeId = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ProfilePhoneId)
                .ForeignKey("dbo.Phone", t => t.PhoneId)
                .ForeignKey("dbo.PhoneType", t => t.PhoneTypeId)
                .ForeignKey("dbo.Profile", t => t.ProfileId)
                .Index(t => t.ProfileId)
                .Index(t => t.PhoneId)
                .Index(t => t.PhoneTypeId);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        PhoneId = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 25),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.PhoneId);
            
            CreateTable(
                "dbo.PhoneType",
                c => new
                    {
                        PhoneTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.PhoneTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProfileAddress", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.ProfilePhone", "ProfileId", "dbo.Profile");
            DropForeignKey("dbo.ProfilePhone", "PhoneTypeId", "dbo.PhoneType");
            DropForeignKey("dbo.ProfilePhone", "PhoneId", "dbo.Phone");
            DropForeignKey("dbo.ProfileAddress", "AddressTypeId", "dbo.AddressType");
            DropForeignKey("dbo.ProfileAddress", "AddressId", "dbo.Address");
            DropIndex("dbo.ProfilePhone", new[] { "PhoneTypeId" });
            DropIndex("dbo.ProfilePhone", new[] { "PhoneId" });
            DropIndex("dbo.ProfilePhone", new[] { "ProfileId" });
            DropIndex("dbo.ProfileAddress", new[] { "AddressTypeId" });
            DropIndex("dbo.ProfileAddress", new[] { "AddressId" });
            DropIndex("dbo.ProfileAddress", new[] { "ProfileId" });
            DropTable("dbo.PhoneType");
            DropTable("dbo.Phone");
            DropTable("dbo.ProfilePhone");
            DropTable("dbo.Profile");
            DropTable("dbo.AddressType");
            DropTable("dbo.ProfileAddress");
            DropTable("dbo.Address");
        }
    }
}
