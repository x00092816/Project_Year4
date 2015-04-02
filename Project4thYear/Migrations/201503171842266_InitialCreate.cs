namespace Project4thYear.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.League",
                c => new
                    {
                        LeagueID = c.Int(nullable: false, identity: true),
                        Year = c.String(),
                        Club = c.String(),
                        Played = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Draws = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                        GoalsFor = c.Int(nullable: false),
                        GoalsAgainst = c.Int(nullable: false),
                        GoalDifference = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Team_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.LeagueID)
                .ForeignKey("dbo.Team", t => t.Team_TeamID)
                .Index(t => t.Team_TeamID);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        TeamID = c.Int(nullable: false, identity: true),
                        LeagueID = c.Int(nullable: false),
                        Year = c.String(),
                        Player = c.String(),
                        Goals = c.Int(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        FirstHalfGoals = c.Int(nullable: false),
                        SecondHalfGoals = c.Int(nullable: false),
                        League_LeagueID = c.Int(),
                        Player1_PlayerID = c.Int(),
                    })
                .PrimaryKey(t => t.TeamID)
                .ForeignKey("dbo.League", t => t.League_LeagueID)
                .ForeignKey("dbo.Player", t => t.Player1_PlayerID)
                .Index(t => t.League_LeagueID)
                .Index(t => t.Player1_PlayerID);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        PlayerID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(nullable: false),
                        Year = c.String(),
                        Competition = c.String(),
                        Club = c.String(),
                        Starts = c.Int(nullable: false),
                        MinutesPlayed = c.Int(nullable: false),
                        YellowCard = c.Int(nullable: false),
                        YellowCardRedCard = c.Int(nullable: false),
                        RedCard = c.Int(nullable: false),
                        Goals = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        OwnGoals = c.Int(nullable: false),
                        Team_TeamID = c.Int(),
                    })
                .PrimaryKey(t => t.PlayerID)
                .ForeignKey("dbo.Team", t => t.Team_TeamID)
                .Index(t => t.Team_TeamID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "Team_TeamID", "dbo.Team");
            DropForeignKey("dbo.Team", "Player1_PlayerID", "dbo.Player");
            DropForeignKey("dbo.League", "Team_TeamID", "dbo.Team");
            DropForeignKey("dbo.Team", "League_LeagueID", "dbo.League");
            DropIndex("dbo.Player", new[] { "Team_TeamID" });
            DropIndex("dbo.Team", new[] { "Player1_PlayerID" });
            DropIndex("dbo.Team", new[] { "League_LeagueID" });
            DropIndex("dbo.League", new[] { "Team_TeamID" });
            DropTable("dbo.Player");
            DropTable("dbo.Team");
            DropTable("dbo.League");
        }
    }
}
