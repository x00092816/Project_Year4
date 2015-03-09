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

namespace Project4thYear.Controllers
{
    public class LeagueController : Controller //: IEnumerable()
    {
        private FootballContext db = new FootballContext();

        //GET: League
        [HttpGet]
        public ActionResult Index()
        {
            //FootballContext db1 = new FootballContext();
            return View(db.Leagues.ToList());
            //return View(db);
        }

       [HttpPost]
        public ActionResult Index()
        {
            return View(db.Leagues.ToList());
        }

       
        //public IEnumerable<String> GetYear(string year)
        //{
        //    var getYear = db.Leagues.Where(y => y.Year == year).Select(y => y.Year);
        //    return getYear;
        //}
        

        //public ActionResult Index()
        //{
        //    var viewModel = new League {db.Leagues.ToList() };
        //    List<League> viewModelList = new List<League> { };
        //    var viewModelDistinct = viewModelList.Distinct().ToList();
        //    return View(viewModelDistinct);
            

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
