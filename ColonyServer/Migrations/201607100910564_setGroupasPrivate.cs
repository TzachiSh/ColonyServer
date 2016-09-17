namespace ColonyServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setGroupasPrivate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGroups", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserGroups", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.UserGroups", new[] { "User_UserId" });
            DropIndex("dbo.UserGroups", new[] { "Group_GroupId" });
            AddColumn("dbo.Users", "Group_GroupId", c => c.Int());
            CreateIndex("dbo.Users", "Group_GroupId");
            AddForeignKey("dbo.Users", "Group_GroupId", "dbo.Groups", "GroupId");
            DropTable("dbo.UserGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Group_GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Group_GroupId });
            
            DropForeignKey("dbo.Users", "Group_GroupId", "dbo.Groups");
            DropIndex("dbo.Users", new[] { "Group_GroupId" });
            DropColumn("dbo.Users", "Group_GroupId");
            CreateIndex("dbo.UserGroups", "Group_GroupId");
            CreateIndex("dbo.UserGroups", "User_UserId");
            AddForeignKey("dbo.UserGroups", "Group_GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.UserGroups", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
