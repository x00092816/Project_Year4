using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectTest2.DAL;
using ProjectTest2.Models;

namespace ProjectTest2.Controllers
{
    public class TeamController : Controller
    {
        private FootballContext db = new FootballContext();

        // GET: Team
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.League).Include(t => t.Player1);
            return View(teams.ToList());
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
            ViewBag.LeagueID = new SelectList(db.Leagues, "LeagueID", "Year");
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "Year");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,LeagueID,PlayerID,Year,Player,Goals,HomeGoals,AwayGoals,FirstHalfGoals,SecondHalfGoals")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LeagueID = new SelectList(db.Leagues, "LeagueID", "Year", team.LeagueID);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "Year", team.PlayerID);
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
            ViewBag.LeagueID = new SelectList(db.Leagues, "LeagueID", "Year", team.LeagueID);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "Year", team.PlayerID);
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,LeagueID,PlayerID,Year,Player,Goals,HomeGoals,AwayGoals,FirstHalfGoals,SecondHalfGoals")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LeagueID = new SelectList(db.Leagues, "LeagueID", "Year", team.LeagueID);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "Year", team.PlayerID);
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
