namespace Project4thYear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamAndPlayerModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Team", "Club", c => c.String());
            AddColumn("dbo.Team", "Person", c => c.String());
            AddColumn("dbo.Player", "Person", c => c.String());
            DropColumn("dbo.Team", "Player");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Team", "Player", c => c.String());
            DropColumn("dbo.Player", "Person");
            DropColumn("dbo.Team", "Person");
            DropColumn("dbo.Team", "Club");
        }
    }
}
