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


        //Pie Chart for GoalFor and GoalsAgainst
        public ActionResult Chart(int? id)
        {
            League l = new League();
            l = db.Leagues.Where(p => p.LeagueID == id).SingleOrDefault();
            var total = from e in db.Leagues
                        where e.LeagueID == id
                        select e;

            int size = total.Count();
            object[] oa1Temp = new object[size];
            object[] oa2Temp = new object[size];
            object[] oa1 = new object[size];
            object[] oa2 = new object[size];
            double val1a = 0;
            double val2a = 0;
            var name1 = "";
            string name2 = "";
            string goalsForString = "Goals For";
            string goalsAgainstString = "Goals Against";
            double totalValue = 0.0;
            double goalsForValue = 0.0;
            double goalsAgainstValue = 0.0;

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                //oa[i] = item.Points;
                oa1Temp[i] = item.GoalsFor;
                oa2Temp[i] = item.GoalsAgainst;
                names[i] = item.Club;
                var val1 = item.GoalsFor;
                var val2 = item.GoalsAgainst;
                name1 = item.Club;


                //Check which value is larger (if statement)

                double DoubleVal1 = Convert.ToDouble(val1);
                double DoubleVal2 = Convert.ToDouble(val2);
                name2 = Convert.ToString(name1);
                goalsForValue = DoubleVal1;
                goalsAgainstValue = DoubleVal2;
                //var hundredPercent = (val1 + val2) / 2;
                //var getPercent = (val2 / val1) * 100;
                //var getLargerNumberPercent = (getPercent / hundredPercent) * 100;
                //var getSmallerNumberPrecent = 100 - getLargerNumberPercent;
                double tempVal1 = DoubleVal1 + DoubleVal2;
                totalValue = tempVal1;
                //double tempVal2 = DoubleVal2 + DoubleVal1;
                //double tempVal3 = tempVal2 / 2;
                //double tempVal4 = tempVal1 / tempVal2;
                //double tempVal5 = tempVal4 * 100;
                //var getLargerNumberPercent = (val1 - val2) / ((val2 + val1) / 2) * 100;
                var getLargerNumberPercent = (DoubleVal1 / tempVal1) * 100;
                //var getLargerNumberPercent = tempVal5;
                var getSmallerNumberPrecent = 100 - getLargerNumberPercent;
                val1a = getLargerNumberPercent;
                val2a = getSmallerNumberPrecent;
                //oa1[i] = getLargerNumberPercent;
                //oa2[i] = getSmallerNumberPrecent;

                //i++;

            }

            //string name = l.Team;
            Highcharts chart = new Highcharts("chart")
               .InitChart(new Chart { PlotShadow = false })
               .SetTitle(new Title { Text = "Home and Away Goals Comparision for: " + name2 })
               .SetSubtitle(new Subtitle { Text = "Total Goals: " + totalValue })
               .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }" })
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
                        new object[] { goalsForString, val1a },
                        new object[] { goalsAgainstString, val2a }
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

      




        //Pie Chart for Win, Draws and Losses

        public ActionResult ChartPie(int? id)
        {
            League l = new League();
            l = db.Leagues.Where(p => p.LeagueID == id).SingleOrDefault();
            var total = from e in db.Leagues
                        where e.LeagueID == id
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
            string winsString = "Wins";
            string drawsString = "Draws";
            string lossesString = "Losses";
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
                var val1 = item.Wins;
                var val2 = item.Draws;
                var val3 = item.Losses;
                name1 = item.Club;


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
               .SetTitle(new Title { Text = "Wins, Draws ans Losses Comparision for: " + name2 })
               .SetSubtitle(new Subtitle { Text = "Total Matches Played: " + totalValue })
               .SetTooltip(new Tooltip { Formatter = "function() { return '<b>'+ this.point.name +'</b>: '+ this.percentage.toFixed(2) +' %'; }" })

               .SetOptions(new GlobalOptions
               {
                   Colors = new[]
		    {
                         ColorTranslator.FromHtml("#FFA500"),
			 ColorTranslator.FromHtml("#7798BF"),
			 ColorTranslator.FromHtml("#55BF3B"),
			 ColorTranslator.FromHtml("#DF5353"),
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
                        new object[] { winsString, val1a },
                        new object[] { drawsString, val2a },
                        new object[] { lossesString, val3a }
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


        //string yearToCheck = "";
          
        public ActionResult ChartBar(string searchString)
        {
            //League l = new League();
            //l = db.Leagues.Where(p => p.Year == searchString).SingleOrDefault();
            //var total = from e in db.Leagues
            //            where e.Year == searchString
            //            select e;

            var total = from r in db.Leagues select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                total = total.Where(s => s.Year.Contains(searchString));
            }

            int size = total.Count();
            object[] oa = new object[size];
            object[] oa2 = new object[size];

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                oa[i] = item.Points;
                names[i] = item.Club;
                i++;

            }

            Highcharts chartBar = new Highcharts("chart")
                .InitChart(new Chart { Type = ChartTypes.Bar })
                .SetTitle(new Title { Text = "League Points Tally" })
                .SetSubtitle(new Subtitle { Text = "Highest Points: " + names[0] })
                .SetXAxis(new XAxis
                {
                    //do for loop here - string[] Categories = new string[names.size]
                    Categories = new[] { names[0], names[1], names[2], names[3], names[4], names[5], names[6], names[7], names[8], names[9], names[10], names[11], names[12], names[13], names[14], names[15], names[16], names[17], names[18], names[19] },
                    
                    Title = new XAxisTitle { Text = "Teams" }
                })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle
                    {
                        Text = "Points",
                        Align = AxisTitleAligns.High
                    }
                })
                
                .SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.name +': '+ this.y +' points'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Bar = new PlotOptionsBar
                    {
                        DataLabels = new PlotOptionsBarDataLabels { Enabled = true }
                    }
                })
                .SetLegend(new Legend
                {
                    Enabled = false,
                    //Layout = Layouts.Vertical,
                    //Align = HorizontalAligns.Right,
                    //VerticalAlign = VerticalAligns.Top,
                    //X = -100,
                    //Y = 100,
                    //Floating = true,
                    //BorderWidth = 1,
                    //BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                    //Shadow = true
                })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series {Data = new Data(new object[] {oa[0], oa[1], oa[2], oa[3], oa[4], oa[5], oa[6], oa[7], oa[8], oa[9], oa[10], oa[11], oa[12], oa[13], oa[14], oa[15], oa[16], oa[17], oa[18], oa[19]}) }
                    //new Series { Name = "Year 1800", Data = new Data(new object[] { 107, 31, 635, 203, 2 }) },
                    //new Series { Name = "Year 1900", Data = new Data(new object[] { 133, 156, 947, 408, 6 }) },
                    //new Series { Name = "Year 2008", Data = new Data(new object[] { 973, 914, 4054, 732, 34 }) }
                });

            return View(chartBar);


            //return View(db.Leagues.ToList());
        }


        //Chart With Negative Values
        public ActionResult ColumnWithNegativeValues(string searchString)
        {
            var total = from r in db.Leagues select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                total = total.Where(s => s.Year.Contains(searchString));
            }

            int size = total.Count();
            object[] oa = new object[size];
            object[] oa2 = new object[size];

            string[] names = new string[size];
            int i = 0;

            foreach (var item in total)
            {
                oa[i] = item.GoalDifference;
                names[i] = item.Club;
                i++;

            }

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = "Column chart with negative values" })
                .SetXAxis(new XAxis { Categories = new[] { names[0], names[1], names[2], names[3], names[4], names[5], names[6], names[7], names[8], names[9], names[10], names[11], names[12], names[13], names[14], names[15], names[16], names[17], names[18], names[19] } })
                //.SetTooltip(new Tooltip { Formatter = "function() { return ''+ this.series.name +': '+ this.y +''; }" })
                .SetCredits(new Credits { Enabled = false })
                .SetSeries(new[]
                {
                    new Series { Data = new Data(new object[] { oa[0], oa[1], oa[2], oa[3], oa[4], oa[5], oa[6], oa[7], oa[8], oa[9], oa[10], oa[11], oa[12], oa[13], oa[14], oa[15], oa[16], oa[17], oa[18], oa[19]}) }
                });

            return View(chart);
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
