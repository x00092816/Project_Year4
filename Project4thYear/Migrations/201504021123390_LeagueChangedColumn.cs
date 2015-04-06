namespace Project4thYear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeagueChangedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.League", "LeagueName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.League", "LeagueName");
        }
    }
}
