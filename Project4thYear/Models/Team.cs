using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project4thYear.Models
{
    public class Team
    {
        //[Key]
        //public int TeamRefID { get; set; }
        public int TeamID { get; set; }
        public int LeagueID { get; set; }
        //public int PlayerID { get; set; }
        [DisplayName("League Name")]
        public string LeagueName { get; set; }
        public string Club { get; set; }
        public string Year { get; set; }
        [DisplayName("Player")]
        public string Person { get; set; }
        public int Goals { get; set; }
        [DisplayName("Home Goals")]
        public int HomeGoals { get; set; }
        [DisplayName("Away Goals")]
        public int AwayGoals { get; set; }
        [DisplayName("First Half Goals")]
        public int FirstHalfGoals { get; set; }
        [DisplayName("Second Half Goals")]
        public int SecondHalfGoals { get; set; }

        //[ForeignKey("LeagueID")]
        public virtual League League { get; set; }

        //[ForeignKey("PlayerID")]
        public virtual Player Player1 { get; set; }

        [InverseProperty("Team")]
        public virtual ICollection<League> Leagues { get; set; }

        [InverseProperty("Team")]
        public virtual ICollection<Player> Players { get; set; }
    }
}