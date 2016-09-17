namespace ColonyServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGroupTableDbSET : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GroupUsers", newName: "UserGroups");
            DropPrimaryKey("dbo.UserGroups");
            AddPrimaryKey("dbo.UserGroups", new[] { "User_UserId", "Group_GroupId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserGroups");
            AddPrimaryKey("dbo.UserGroups", new[] { "Group_GroupId", "User_UserId" });
            RenameTable(name: "dbo.UserGroups", newName: "GroupUsers");
        }
    }
}
