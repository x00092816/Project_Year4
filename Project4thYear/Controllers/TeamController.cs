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
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Enums;

namespace Project4thYear.Controllers
{
    public class TeamController : Controller
    {
        private FootballContext db = new FootballContext();

        // GET: Team
        public ActionResult Index(string searchString)
        {
            var res = from r in db.Teams select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(s => s.Person.Contains(searchString));
            }
            return View(res);
            //return View(db.Teams.ToList());
        }


        //Semi Circle Pie Chart for Home Goals and Away Goals

        public ActionResult SemiCircleDonut(int? id)
        {
            Team l = new Team();
            l = db.Teams.Where(p => p.TeamID == id).SingleOrDefault();
            var total = from e in db.Teams
                        where e.TeamID == id
                        select e;

            int size = total.Count();
            object[] oa1Temp = new object[size];
            object[] oa2Temp = new object[size];
            object[] oa1 = new object[size];
            object[] oa2 = new object[size];
            double val1a = 0;
            double val2a = 0;
            var name1 = "";
            string HGoalsString = "Home Goals";
            string AGoalsString = "Away Goals";
            double totalValue = 0.0;
            double HGoalsValue = 0.0;
            double AGoalsValue = 0.0;

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                names[i] = item.Person;
                var val1 = item.HomeGoals;
                var val2 = item.AwayGoals;
                name1 = item.Person;


                //Check which value is larger (if statement)

                double DoubleVal1 = Convert.ToDouble(val1);
                double DoubleVal2 = Convert.ToDouble(val2);
                HGoalsValue = DoubleVal1;
                AGoalsValue = DoubleVal2;
     
                double tempVal1 = DoubleVal1 + DoubleVal2;
                totalValue = tempVal1;
                var getPercent1 = (DoubleVal1 / tempVal1) * 100;
                var getPercent2 = (DoubleVal2 / tempVal1) * 100;
                val1a = getPercent1;
                val2a = getPercent2;
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
                    Name = names[0] + " Home v Away Goals Percentage: ",
                    PlotOptionsPie = new PlotOptionsPie { InnerSize = new PercentageOrPixel(50, true) },
                    Data = new Data(new object[]
                    {
                        new object[] { HGoalsString, val1a },
                        new object[] { AGoalsString, val2a }
                        
                    })
                });

            return View(chart);
        }


        //Semi circle Pie Chart for First Half Goals v Second Half Goals
        public ActionResult SemiCircleDonut2(int? id)
        {
            Team l = new Team();
            l = db.Teams.Where(p => p.TeamID == id).SingleOrDefault();
            var total = from e in db.Teams
                        where e.TeamID == id
                        select e;

            int size = total.Count();
            object[] oa1Temp = new object[size];
            object[] oa2Temp = new object[size];
            object[] oa1 = new object[size];
            object[] oa2 = new object[size];
            double val1a = 0;
            double val2a = 0;
            var name1 = "";
            string FHGoalsString = "First Half Goals";
            string SHGoalsString = "Second Half Goals";
            double totalValue = 0.0;

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                names[i] = item.Person;
                var val1 = item.FirstHalfGoals;
                var val2 = item.SecondHalfGoals;
                name1 = item.Person;


                //Check which value is larger (if statement)

                double DoubleVal1 = Convert.ToDouble(val1);
                double DoubleVal2 = Convert.ToDouble(val2);
                

                double tempVal1 = DoubleVal1 + DoubleVal2;
                totalValue = tempVal1;
                var getPercent1 = (DoubleVal1 / tempVal1) * 100;
                var getPercent2 = (DoubleVal2 / tempVal1) * 100;
                val1a = getPercent1;
                val2a = getPercent2;


                //i++;
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
                    Name = names[0] + " Home v Away Goals Percentage: ",
                    PlotOptionsPie = new PlotOptionsPie { InnerSize = new PercentageOrPixel(50, true) },
                    Data = new Data(new object[]
                    {
                        new object[] { FHGoalsString, val1a },
                        new object[] { SHGoalsString, val2a }
                        
                    })
                });

            return View(chart);
        }



        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,LeagueID,Year,Player,Goals,HomeGoals,AwayGoals,FirstHalfGoals,SecondHalfGoals")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,LeagueID,Year,Player,Goals,HomeGoals,AwayGoals,FirstHalfGoals,SecondHalfGoals")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
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
