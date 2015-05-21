using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameStats.DataAccess;
using GameStats.Models;

namespace GameStats.Controllers
{
    public class GameController : Controller
    {
        private Context db = new Context();

        // GET: /Game/
        public ActionResult Index()
        {
            List<string> list = new List<string>();
            foreach (var item in db.GAMES)
            {
                list.Add((from a in db.CATEGORIES where a.ID == item.CATEGORY_ID select a.NAME).FirstOrDefault());
            }

            ViewBag.CATEGORY = list;
            return View(db.GAMES.ToList());
        }

        // GET: /Game/Details/5
        public ActionResult Details(int? id)
        {
            List<string> list = new List<string>();
            int i = 1;
            foreach (var item in db.CATEGORIES)
            {
                list.Add((from a in db.CATEGORIES where a.ID == i select a.NAME).FirstOrDefault());
                i++;
            }
            ViewBag.CATEGORY = list;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME game = db.GAMES.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: /Game/Create
        public ActionResult Create(int? id)
        {
            ViewBag.CATEGORY = new SelectList(db.CATEGORIES, "ID", "NAME");
            return View();
        }

        // POST: /Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,DESCRIPTION,CATEGORY, CATEGORY_ID")] GameCategoryViewModel model, [Bind(Include = "ID,NAME,DESCRIPTION,CATEGORY_ID")] GAME game)
        {
            game.NAME = model.NAME;
            game.DESCRIPTION = model.DESCRIPTION;
            game.CATEGORY_ID = model.CATEGORY_ID;

            if (ModelState.IsValid)
            {
                db.GAMES.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: /Game/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.CATEGORY = new SelectList(db.CATEGORIES, "ID", "NAME");

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME game = db.GAMES.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: /Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,NAME,DESCRIPTION,CATEGORY_ID")] GAME game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: /Game/Delete/5
        public ActionResult Delete(int? id)
        {
            List<string> list = new List<string>();
            int i = 1;
            foreach (var item in db.CATEGORIES)
            {
                list.Add((from a in db.CATEGORIES where a.ID == i select a.NAME).FirstOrDefault());
                i++;
            }

            ViewBag.CATEGORY = list;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME game = db.GAMES.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: /Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GAME game = db.GAMES.Find(id);
            db.GAMES.Remove(game);
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
