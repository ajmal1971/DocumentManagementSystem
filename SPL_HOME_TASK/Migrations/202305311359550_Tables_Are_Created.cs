namespace SPL_HOME_TASK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tables_Are_Created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocumentCategoryInfoes",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 150),
                        CategoryNameBangla = c.String(maxLength: 250),
                        Description = c.String(maxLength: 500),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .Index(t => t.CategoryName, unique: true);
            
            CreateTable(
                "dbo.DocumentInformations",
                c => new
                    {
                        DocumentIdentity = c.Long(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        DocumentReferenceName = c.String(nullable: false, maxLength: 150),
                        DocumentDate = c.DateTime(nullable: false),
                        DocumentName = c.String(nullable: false, maxLength: 250),
                        DocumentNameBangla = c.String(maxLength: 500),
                        Description = c.String(maxLength: 1500),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DocumentIdentity)
                .Index(t => new { t.CategoryId, t.DocumentName }, unique: true, name: "IX_UniqueKey");
            
            CreateTable(
                "dbo.FileInformations",
                c => new
                    {
                        FileIdentity = c.Long(nullable: false, identity: true),
                        DocumentIdentity = c.Long(nullable: false),
                        FileNo = c.String(maxLength: 50),
                        FileName = c.String(nullable: false, maxLength: 250),
                        FileNameBangla = c.String(maxLength: 450),
                        Description = c.String(maxLength: 500),
                        FilePath = c.String(maxLength: 250),
                        FileStatus = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.FileIdentity)
                .ForeignKey("dbo.DocumentInformations", t => t.DocumentIdentity, cascadeDelete: true)
                .Index(t => new { t.DocumentIdentity, t.FileName }, unique: true, name: "IX_UniqueKey");
            
            CreateTable(
                "dbo.MetaDataInformations",
                c => new
                    {
                        MetaIdentity = c.Long(nullable: false, identity: true),
                        DocumentIdentity = c.Long(nullable: false),
                        MetaName = c.String(nullable: false, maxLength: 150),
                        MetaNameBangla = c.String(maxLength: 250),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.MetaIdentity)
                .ForeignKey("dbo.DocumentInformations", t => t.DocumentIdentity, cascadeDelete: true)
                .Index(t => new { t.DocumentIdentity, t.MetaName }, unique: true, name: "IX_UniqueKey");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetaDataInformations", "DocumentIdentity", "dbo.DocumentInformations");
            DropForeignKey("dbo.FileInformations", "DocumentIdentity", "dbo.DocumentInformations");
            DropIndex("dbo.MetaDataInformations", "IX_UniqueKey");
            DropIndex("dbo.FileInformations", "IX_UniqueKey");
            DropIndex("dbo.DocumentInformations", "IX_UniqueKey");
            DropIndex("dbo.DocumentCategoryInfoes", new[] { "CategoryName" });
            DropTable("dbo.MetaDataInformations");
            DropTable("dbo.FileInformations");
            DropTable("dbo.DocumentInformations");
            DropTable("dbo.DocumentCategoryInfoes");
        }
    }
}
