namespace TailgateLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmigrations : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TeamUsers", newName: "UserTeams");
            DropPrimaryKey("dbo.UserTeams");
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comments = c.String(),
                        UserId = c.Int(nullable: false),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EventId);
            
            AddColumn("dbo.Events", "TeamId", c => c.Int(nullable: false));
            AddColumn("dbo.Teams", "Event_Id", c => c.Int());
            AddPrimaryKey("dbo.UserTeams", new[] { "User_Id", "Team_Id" });
            CreateIndex("dbo.Events", "TeamId");
            CreateIndex("dbo.Teams", "Event_Id");
            AddForeignKey("dbo.Events", "TeamId", "dbo.Teams", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Teams", "Event_Id", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "EventId", "dbo.Events");
            DropForeignKey("dbo.Teams", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "TeamId", "dbo.Teams");
            DropIndex("dbo.Teams", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "TeamId" });
            DropIndex("dbo.Comments", new[] { "EventId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropPrimaryKey("dbo.UserTeams");
            DropColumn("dbo.Teams", "Event_Id");
            DropColumn("dbo.Events", "TeamId");
            DropTable("dbo.Comments");
            AddPrimaryKey("dbo.UserTeams", new[] { "Team_Id", "User_Id" });
            RenameTable(name: "dbo.UserTeams", newName: "TeamUsers");
        }
    }
}
