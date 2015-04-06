namespace Project4thYear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDsRemoved : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.League", "LeagueRefID");
            DropColumn("dbo.Team", "TeamRefID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Team", "TeamRefID", c => c.Int(nullable: false));
            AddColumn("dbo.League", "LeagueRefID", c => c.Int(nullable: false));
        }
    }
}
