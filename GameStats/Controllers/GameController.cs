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
            List<string> games = new List<string>();
            foreach (var item in db.GAMES)
            {
                if ((from a in db.GAMES where a.ID == item.ID select a.CATEGORY_ID).FirstOrDefault() != null)
                {
                    string tempCategories = (from a in db.GAMES where a.ID == item.ID select a.CATEGORY_ID).FirstOrDefault();
                    string[] categories = tempCategories.Split(',');
                    categories = categories.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    int[] catID = new int[categories.Length];
                    for (int i = 0; i < categories.Length; i++)
                    {
                        catID[i] = Convert.ToInt32(categories[i]);
                    }
                    string allCategories = null;
                    foreach (var id in catID)
                    {
                        allCategories += (from a in db.CATEGORIES where a.ID == id select a.NAME).FirstOrDefault() + ", ";
                    }
                    allCategories = allCategories.Remove(allCategories.Length - 2);
                    games.Add(allCategories);
                }
                else
                {
                    games.Add(" ");
                }
            }

            ViewBag.CATEGORY = games;
            return View(db.GAMES.ToList());
        }

        // GET: /Game/Details/5
        public ActionResult Details(int? id)
        {
            string allCategories = null;
            if ((from a in db.GAMES where a.ID == id select a.CATEGORY_ID).FirstOrDefault() != null)
            {
                string tempCategories = (from a in db.GAMES where a.ID == id select a.CATEGORY_ID).FirstOrDefault();
                string[] categories = tempCategories.Split(',');
                categories = categories.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int[] catID = new int[categories.Length];
                for (int i = 0; i < categories.Length; i++)
                {
                    catID[i] = Convert.ToInt32(categories[i]);
                }

                foreach (var categoryId in catID)
                {
                    allCategories += (from a in db.CATEGORIES where a.ID == categoryId select a.NAME).FirstOrDefault() + ", ";
                }
                allCategories = allCategories.Remove(allCategories.Length - 2);
            }
            ViewBag.CATEGORY = allCategories;

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
            ViewBag.CATEGORY = (from a in db.CATEGORIES select a);
            return View();
        }

        // POST: /Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int[] category, [Bind(Include = "ID,NAME,DESCRIPTION,CATEGORY, CATEGORY_ID")] GameCategoryViewModel model, [Bind(Include = "ID,NAME,DESCRIPTION,CATEGORY_ID")] GAME game)
        {
            game.NAME = model.NAME;
            game.DESCRIPTION = model.DESCRIPTION;
            if (category != null)
            {
                for (int i = 0; i < category.Length; i++)
                {
                    game.CATEGORY_ID += category[i] + ",";
                }
            }
            else
            {
                game.CATEGORY_ID = null;
            }

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
            if ((from a in db.GAMES where a.ID == id select a.CATEGORY_ID).FirstOrDefault() != null)
            {
                string tempCategories = (from a in db.GAMES where a.ID == id select a.CATEGORY_ID).FirstOrDefault();
                string[] categories = tempCategories.Split(',');
                categories = categories.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int[] catID = new int[categories.Length];
                for (int i = 0; i < categories.Length; i++)
                {
                    catID[i] = Convert.ToInt32(categories[i]);
                }
                List<string> check = new List<string>();
                int categoriesCount = 0;
                foreach (var item in db.CATEGORIES)
                {
                    categoriesCount++;
                }
                bool isChecked = false;
                foreach (var item in (from a in db.CATEGORIES select a.ID))
                {
                    for (int j = 0; j < catID.Length; j++)
                    {
                        if ((from a in db.CATEGORIES where a.ID == item select a.ID).FirstOrDefault() == catID[j])
                        {
                            check.Add("checked");
                            isChecked = true;
                        }
                    }
                    if (isChecked == false)
                    {
                        check.Add("");
                    }
                    isChecked = false;
                }
                ViewBag.isChecked = check;
            }
            else
            {
                int categoriesCount = 0;
                foreach (var item in db.CATEGORIES)
                {
                    categoriesCount++;
                }
                ViewBag.isChecked = new string[categoriesCount];
            }
            ViewBag.CATEGORY = (from a in db.CATEGORIES select a);

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

        public ActionResult Edit(int[] category, [Bind(Include = "ID,NAME,DESCRIPTION,CATEGORY_ID")] GAME game)
        {
            if (category != null)
            {
                for (int i = 0; i < category.Length; i++)
                {
                    game.CATEGORY_ID += category[i] + ",";
                }
            }
            else
            {
                game.CATEGORY_ID = null;
            }

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
            string allCategories = null;
            if ((from a in db.GAMES where a.ID == id select a.CATEGORY_ID).FirstOrDefault() != null)
            {
                string tempCategories = (from a in db.GAMES where a.ID == id select a.CATEGORY_ID).FirstOrDefault();
                string[] categories = tempCategories.Split(',');
                categories = categories.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                int[] catID = new int[categories.Length];
                for (int i = 0; i < categories.Length; i++)
                {
                    catID[i] = Convert.ToInt32(categories[i]);
                }

                foreach (var categoryId in catID)
                {
                    allCategories += (from a in db.CATEGORIES where a.ID == categoryId select a.NAME).FirstOrDefault() + ", ";
                }
                allCategories = allCategories.Remove(allCategories.Length - 2);
            }
            ViewBag.CATEGORY = allCategories;

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
