namespace TSModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50),
                        Teatcher_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentTeacher",
                c => new
                    {
                        StudentRefId = c.Int(nullable: false),
                        TeacherRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentRefId, t.TeacherRefId })
                .ForeignKey("dbo.Student", t => t.StudentRefId, cascadeDelete: true)
                .ForeignKey("dbo.Teacher", t => t.TeacherRefId, cascadeDelete: true)
                .Index(t => t.StudentRefId)
                .Index(t => t.TeacherRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentTeacher", "TeacherRefId", "dbo.Teacher");
            DropForeignKey("dbo.StudentTeacher", "StudentRefId", "dbo.Student");
            DropIndex("dbo.StudentTeacher", new[] { "TeacherRefId" });
            DropIndex("dbo.StudentTeacher", new[] { "StudentRefId" });
            DropTable("dbo.StudentTeacher");
            DropTable("dbo.Teacher");
            DropTable("dbo.Student");
        }
    }
}
