using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project4thYear.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public int TeamID { get; set; }
        public string Year { get; set; }
        public string Competition { get; set; }
        public string Person { get; set; }
        public string Club { get; set; }
        public int Starts { get; set; }
        public int MinutesPlayed { get; set; }
        public int YellowCard { get; set; }
        public int YellowCardRedCard { get; set; }
        public int RedCard { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int OwnGoals { get; set; }

        //[ForeignKey("TeamID")]
        public virtual Team Team { get; set; }

        //[ForeignKey("LeagueID")]
        //public virtual League League { get; set; }

        //[InverseProperty("League")]
        //public virtual ICollection<League> Leagues { get; set; }
    }
}