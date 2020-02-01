namespace TSModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forign : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Student", "Teatcher_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Student", "Teatcher_Id", c => c.Int(nullable: false));
        }
    }
}
