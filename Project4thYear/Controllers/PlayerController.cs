using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project4thYear.DAL;
using Project4thYear.Models;
using DotNet.Highcharts.Options;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;
using System.Drawing;

namespace Project4thYear.Controllers
{
    public class PlayerController : Controller
    {
        private FootballContext db = new FootballContext();

        // GET: Player
        public ActionResult Index(string searchString)
        {
            var res = from r in db.Players select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(s => s.Person.Contains(searchString));
            }
            return View(res);
            
            //return View(db.Players.ToList());
        }


        // Pie Chart for 
        public ActionResult SemiCircleDonutPlayer(int? id)
        {
            Player l = new Player();
            l = db.Players.Where(p => p.PlayerID == id).SingleOrDefault();
            var total = from e in db.Players
                        where e.PlayerID == id
                        select e;

            int size = total.Count();
            object[] oa1Temp = new object[size];
            object[] oa2Temp = new object[size];
            object[] oa1 = new object[size];
            object[] oa2 = new object[size];
            double val1a = 0;
            double val2a = 0;
            double val3a = 0;
            var name1 = "";
            string GoalsString = "Goals";
            string AssistsString = "Assists";
            string OwnGoalsString = "Own Goals";
            double totalValue = 0.0;
            //double HGoalsValue = 0.0;
            //double AGoalsValue = 0.0;

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                names[i] = item.Person;
                var val1 = item.Goals;
                var val2 = item.Assists;
                var val3 = item.OwnGoals;
                name1 = item.Person;


                //Check which value is larger (if statement)

                double DoubleVal1 = Convert.ToDouble(val1);
                double DoubleVal2 = Convert.ToDouble(val2);
                double DoubleVal3 = Convert.ToDouble(val3);
                //YCardValue = DoubleVal1;
                //AGoalsValue = DoubleVal2;

                double tempVal1 = DoubleVal1 + DoubleVal2;
                totalValue = tempVal1;
                var getPercent1 = (DoubleVal1 / tempVal1) * 100;
                var getPercent2 = (DoubleVal2 / tempVal1) * 100;
                var getPercent3 = (DoubleVal3 / tempVal1) * 100;
                val1a = getPercent1;
                val2a = getPercent2;
                val3a = getPercent3;
            }


            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { PlotBorderWidth = 0, PlotShadow = false })
                .SetTitle(new Title { Text = names[0], Align = HorizontalAligns.Center, VerticalAlign = VerticalAligns.Middle, Y = 50 })
                .SetTooltip(new Tooltip { PointFormat = "{series.name}: <b>{point.percentage:.1f}%</b>" })
                .SetPlotOptions(new PlotOptions
                {
                    Pie = new PlotOptionsPie
                    {
                        StartAngle = -90,
                        EndAngle = 90,
                        Center = new[] { new PercentageOrPixel(50, true), new PercentageOrPixel(75, true) },
                        DataLabels = new PlotOptionsPieDataLabels
                        {
                            Enabled = true,
                            Distance = -50,
                            Style = "fontWeight: 'bold', color: 'white', textShadow: '0px 1px 2px black'"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = names[0] + " Goals v Assists v Own Goals Percentage: ",
                    PlotOptionsPie = new PlotOptionsPie { InnerSize = new PercentageOrPixel(50, true) },
                    Data = new Data(new object[]
                    {
                        new object[] { GoalsString, val1a },
                        new object[] { AssistsString, val2a },
                        new object[] { OwnGoalsString, val3a }
                        
                    })
                });

            return View(chart);
        }



        //Pie Chart for Cards
        public ActionResult ChartPiePlayer(int? id)
        {
            Player l = new Player();
            l = db.Players.Where(p => p.PlayerID == id).SingleOrDefault();
            var total = from e in db.Players
                        where e.PlayerID == id
                        select e;

            int size = total.Count();
            object[] oa1Temp = new object[size];
            object[] oa2Temp = new object[size];
            object[] oa1 = new object[size];
            object[] oa2 = new object[size];
            double val1a = 0;
            double val2a = 0;
            double val3a = 0;
            var name1 = "";
            string name2 = "";
            string YCardString = "Yellow Card";
            string DYCardString = "Double Yellow Card";
            string RCardString = "Red Card";
            double totalValue = 0.0;
            double winsValue = 0.0;
            double drawsValue = 0.0;
            double lossesValue = 0.0;

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                //oa[i] = item.Points;
                //oa1Temp[i] = item.GoalsFor;
                //oa2Temp[i] = item.GoalsAgainst;
                names[i] = item.Club;
                var val1 = item.YellowCard;
                var val2 = item.YellowCardRedCard;
                var val3 = item.RedCard;
                name1 = item.Person;


                //Check which value is larger (if statement)

                double DoubleVal1 = Convert.ToDouble(val1);
                double DoubleVal2 = Convert.ToDouble(val2);
                double DoubleVal3 = Convert.ToDouble(val3);
                name2 = Convert.ToString(name1);
                winsValue = DoubleVal1;
                drawsValue = DoubleVal2;
                lossesValue = DoubleVal3;
                //var hundredPercent = (val1 + val2) / 2;
                //var getPercent = (val2 / val1) * 100;
                //var getLargerNumberPercent = (getPercent / hundredPercent) * 100;
                //var getSmallerNumberPrecent = 100 - getLargerNumberPercent;
                double tempVal1 = DoubleVal1 + DoubleVal2 + DoubleVal3;
                totalValue = tempVal1;
                //double tempVal2 = DoubleVal2 + DoubleVal1;
                //double tempVal3 = tempVal2 / 2;
                //double tempVal4 = tempVal1 / tempVal2;
                //double tempVal5 = tempVal4 * 100;
                //var getLargerNumberPercent = (val1 - val2) / ((val2 + val1) / 2) * 100;
                var getPercent1 = (DoubleVal1 / tempVal1) * 100;
                var getPercent2 = (DoubleVal2 / tempVal1) * 100;
                var getPercent3 = (DoubleVal3 / tempVal1) * 100;
                //var getLargerNumberPercent = tempVal5;
                //var getSmallerNumberPrecent = 100 - getLargerNumberPercent;
                val1a = getPercent1;
                val2a = getPercent2;
                val3a = getPercent3;
                //oa1[i] = getLargerNumberPercent;
                //oa2[i] = getSmallerNumberPrecent;

                //i++;

            }

            //string name = l.Team;
            Highcharts chart = new Highcharts("chart")
               .InitChart(new Chart { PlotShadow = false })
               .SetTitle(new Title { Text = "Cards Comparision for: " + name2 })
               .SetSubtitle(new Subtitle { Text = "Total Cards Recieved: " + totalValue })
               .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }" })

               .SetOptions(new GlobalOptions
               {
                   Colors = new[]
		    {
                         ColorTranslator.FromHtml("#FFA500"),
			 ColorTranslator.FromHtml("#7798BF"),
			 ColorTranslator.FromHtml("#FF0000"),
			 ColorTranslator.FromHtml("#55BFB3"),
			 ColorTranslator.FromHtml("#DDDF0D"),
			 ColorTranslator.FromHtml("#aaeeee"),
			 ColorTranslator.FromHtml("#ff0066"),
			 ColorTranslator.FromHtml("#eeaaee")
		    }
               })
               .SetPlotOptions(new PlotOptions
               {
                   Pie = new PlotOptionsPie
                   {
                       AllowPointSelect = true,
                       Cursor = Cursors.Pointer,
                       DataLabels = new PlotOptionsPieDataLabels
                       {
                           Color = ColorTranslator.FromHtml("#000000"),
                           ConnectorColor = ColorTranslator.FromHtml("#000000"),
                           Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }"
                       }
                   }
               })
               .SetSeries(new Series
               {
                   Type = ChartTypes.Pie,
                   Name = "",
                   Data = new Data(new object[]
                    {
                        new object[] { YCardString, val1a },
                        new object[] { DYCardString, val2a },
                        new object[] { RCardString, val3a }
                        //new DotNet.Highcharts.Options.Point
                        //{
                        //    Name = "Chrome",
                        //    Y = 12.8,
                        //    Sliced = true,
                        //    Selected = true
                        //},
                        //new object[] { "Safari", 8.5 },
                        //new object[] { "Opera", 6.2 },
                        //new object[] { "Others", 0.7 }
                    })
               });

            return View(chart);
        }






        // GET: Player/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Player/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerID,TeamID,Year,Competition,Club,Starts,MinutesPlayed,YellowCard,YellowCardRedCard,RedCard,Goals,Assists,OwnGoals")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Players.Add(player);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerID,TeamID,Year,Competition,Club,Starts,MinutesPlayed,YellowCard,YellowCardRedCard,RedCard,Goals,Assists,OwnGoals")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Player/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
