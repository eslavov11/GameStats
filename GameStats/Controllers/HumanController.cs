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
    public class HumanController : Controller
    {
        private Context db = new Context();

        // GET: /Human/
        public ActionResult Index()
        {
            return View(db.HUMANS.ToList());
        }

        // GET: /Human/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUMAN human = db.HUMANS.Find(id);
            if (human == null)
            {
                return HttpNotFound();
            }
            return View(human);
        }

        // GET: /Human/Create
        public ActionResult Create()
        {
            var model = new HumanViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(HumanViewModel model, [Bind(Include = "ID,FIRST_NAME,SECOND_NAME,LAST_NAME,EMAIL,PHONE_NUMBER,DATE_OF_BIRTH,PICTURE")] HUMAN human)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            byte[] uploadedFile = new byte[model.File.InputStream.Length];
            model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

            human.PICTURE = uploadedFile;

            if (ModelState.IsValid)
            {
                db.HUMANS.Add(human);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(human);
        }

        // GET: /Human/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUMAN human = db.HUMANS.Find(id);
            if (human == null)
            {
                return HttpNotFound();
            }
            return View(human);
        }

        // POST: /Human/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,FIRST_NAME,SECOND_NAME,LAST_NAME,EMAIL,PHONE_NUMBER,DATE_OF_BIRTH,PICTURE")] HUMAN human)
        {
            if (ModelState.IsValid)
            {
                db.Entry(human).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(human);
        }

        // GET: /Human/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUMAN human = db.HUMANS.Find(id);
            if (human == null)
            {
                return HttpNotFound();
            }
            return View(human);
        }

        // POST: /Human/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HUMAN human = db.HUMANS.Find(id);
            db.HUMANS.Remove(human);
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
