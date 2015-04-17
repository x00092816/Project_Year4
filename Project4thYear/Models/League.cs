using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project4thYear.Models
{
    public class League
    {
        //[Key]
        //public int LeagueRefID { get; set; }
        public int LeagueID { get; set; }
        //public int TeamID { get; set; }
        public String LeagueName { get; set; }
        public string Year { get; set; }
        public string Club { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }

        //[ForeignKey("TeamID")]
        public virtual Team Team { get; set; }

        //[ForeignKey("PlayerID")]
        //public virtual Player Player { get; set; }

        //[InverseProperty("Player")]
        //public virtual ICollection<Player> Players { get; set; }
    }
}