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
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;
using System.Drawing;
using DotNet.Highcharts;

namespace Project4thYear.Controllers
{
    public class LeagueController : Controller //: IEnumerable()
    {
        private FootballContext db = new FootballContext();

        //GET: League
        public ActionResult Index(string searchString)
        {
            var res = from r in db.Leagues select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                res = res.Where(s => s.Year.Contains(searchString));
            }
            return View(res);
            //return View(db.Leagues.ToList());
        }

        public ActionResult Chart(int? id)
        {
            League l = new League();
            l = db.Leagues.Where(p => p.LeagueID == id).SingleOrDefault();
            var total = from e in db.Leagues
                        where e.LeagueID == id
                        select e;

            int size = total.Count();
            object[] oa1 = new object[size];
            object[] oa2 = new object[size];

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                //oa[i] = item.Points;
                oa1[i] = item.GoalsFor;
                oa2[i] = item.GoalsAgainst;
                names[i] = item.Club;
                i++;

            }
        
            //string name = l.Team;
             Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { PlotShadow = false })
                .SetTitle(new Title { Text = "Home and Away Goals Comparision" })
                .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage +' %'; }" })
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
                            Formatter = "function() { }"
                        }
                    }
                })
                .SetSeries(new Series
                {
                    Type = ChartTypes.Pie,
                    Name = "",
                    Data = new Data(new object[]
                    {
                        new object[] { oa1 },
                        new object[] { oa2 }
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
            
        



            //Highcharts chart = new Highcharts("chart")
            //    .InitChart(new Chart { Type = ChartTypes.Bar })
            //    .SetTitle(new Title { Text = "" })
            //    .SetSubtitle(new Subtitle { Text = "" })
            //    .SetXAxis(new XAxis
            //    {
            //           //do for loop here - string[] Categories = new string[names.size]
            //        Categories = new[] { names[0] },
            //        Title = new XAxisTitle { Text = string.Empty }
            //    })
            //    .SetYAxis(new YAxis
            //    {
            //        Min = 0,
            //        Title = new YAxisTitle
            //        {
            //            Text = "Points",
            //            Align = AxisTitleAligns.High
            //        }
            //    })
            //    .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +' points'; }" })
            //    .SetPlotOptions(new PlotOptions
            //    {
            //        Bar = new PlotOptionsBar
            //        {
            //            DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
            //        }
            //    })
            //    .SetLegend(new Legend
            //    {
            //        Enabled = false
            //        //Layout = Layouts.Vertical,
            //        //Align = HorizontalAligns.Right,
            //        //VerticalAlign = VerticalAligns.Top,
            //        //X = -100,
            //        //Y = 100,
            //        //Floating = true,
            //        //BorderWidth = 1,
            //        //BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
            //        //Shadow = true
            //    })
            //    .SetCredits(new Credits { Enabled = false })
            //    .SetSeries(new[]
            //    {
            //        new Series {Data = new Data(new object[] {oa}) }
            //        //new Series { Name = "Year 1800", Data = new Data(new object[] { 107, 31, 635, 203, 2 }) },
            //        //new Series { Name = "Year 1900", Data = new Data(new object[] { 133, 156, 947, 408, 6 }) },
            //        //new Series { Name = "Year 2008", Data = new Data(new object[] { 973, 914, 4054, 732, 34 }) }
            //    });

            //return View(chart);


            //return View(db.Leagues.ToList());
        }

        //public ActionResult Chart(int? id)
        //{
        //    Team t = new Team();
        //    t = db.Teams.Where(p => p.TeamID == id).SingleOrDefault();
        //    var total = from e in db.Teams
        //                where e.TeamID == id
        //                select e;

        //    int size = total.Count();
        //    object[] array = new object[size];
        //    int i = 0;

        //    foreach (var item in total)
        //    {
        //        array[i] = item.Goals;
        //        i++;
        //    }

            
        //    return View(db.Leagues.ToList());
        //}
        //GET: League
        //[HttpGet]
        //public ActionResult LeagueGet()
        //{
        //    //var year = db.Leagues.Find("2013/2014");
        //    return View(db.Leagues.Where(y);
        //}

        //public ActionResult Results13_14()
        //{
        //    return View(db.Leagues.ToList());
        //}

        //public ActionResult Results12_13()
        //{
        //    return View(db.Leagues.ToList());
        //}

        //public IEnumerable<String> GetYear(string year)
        //{
        //    var getYear = db.Leagues.Where(y => y.Year == year).Select(y => y.Year);
        //    return getYear;
        //}


        //public ActionResult Index()
        //{
        //    var viewModel = new League { db.Leagues.ToList() };
        //    List<League> viewModelList = new List<League> { };
        //    var viewModelDistinct = viewModelList.Distinct().ToList();
        //    return View(viewModelDistinct);
        //}

        //    var viewModel = new League
        //    {
        //        db.Leagues.ToList();
        //    }
        //    List<League> viewModelList = new List<League>
        //    viewModelList.Add(viewModel);
        //    return View(viewModel);
        //    return View(viewModel);
        //}

        // GET: League/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        // GET: League/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: League/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LeagueID,Year,Club,Played,Wins,Draws,Losses,GoalsFor,GoalsAgainst,GoalDifference,Points")] League league)
        {
            if (ModelState.IsValid)
            {
                db.Leagues.Add(league);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(league);
        }

        // GET: League/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        // POST: League/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LeagueID,Year,Club,Played,Wins,Draws,Losses,GoalsFor,GoalsAgainst,GoalDifference,Points")] League league)
        {
            if (ModelState.IsValid)
            {
                db.Entry(league).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(league);
        }

        // GET: League/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            League league = db.Leagues.Find(id);
            if (league == null)
            {
                return HttpNotFound();
            }
            return View(league);
        }

        // POST: League/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            League league = db.Leagues.Find(id);
            db.Leagues.Remove(league);
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
