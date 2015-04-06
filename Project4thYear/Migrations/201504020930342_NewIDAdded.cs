namespace Project4thYear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewIDAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.League", "LeagueRefID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Team", "TeamRefID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Team", "LeagueName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Team", "LeagueName");
            DropColumn("dbo.Team", "TeamRefID");
            DropColumn("dbo.League", "LeagueRefID");
        }
    }
}
