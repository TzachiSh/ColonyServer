namespace ColonyServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addManyToManyUserGroup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.Users", new[] { "Group_GroupId" });
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Group_GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Group_GroupId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_GroupId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Group_GroupId);
            
            DropColumn("dbo.Users", "Group_GroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Group_GroupId", c => c.Int());
            DropForeignKey("dbo.UserGroups", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.UserGroups", "User_UserId", "dbo.Users");
            DropIndex("dbo.UserGroups", new[] { "Group_GroupId" });
            DropIndex("dbo.UserGroups", new[] { "User_UserId" });
            DropTable("dbo.UserGroups");
            CreateIndex("dbo.Users", "Group_GroupId");
            AddForeignKey("dbo.Users", "Group_GroupId", "dbo.Groups", "GroupId");
        }
    }
}
