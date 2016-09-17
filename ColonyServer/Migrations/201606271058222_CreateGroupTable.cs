namespace ColonyServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGroupTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        Created = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Group_GroupId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_GroupId, t.User_UserId })
                .ForeignKey("dbo.Groups", t => t.Group_GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Group_GroupId)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.GroupUsers", new[] { "User_UserId" });
            DropIndex("dbo.GroupUsers", new[] { "Group_GroupId" });
            DropTable("dbo.GroupUsers");
            DropTable("dbo.Groups");
        }
    }
}
