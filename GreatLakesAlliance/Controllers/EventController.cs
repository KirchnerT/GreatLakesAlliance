using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreatLakesAlliance.Models;

namespace GreatLakesAlliance.Controllers
{
    public class EventController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Event
        public ActionResult Index()
        {
            return View(db.EventDataModels.ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDataModel eventDataModel = db.EventDataModels.Find(id);
            if (eventDataModel == null)
            {
                return HttpNotFound();
            }
            return View(eventDataModel);
        }

        // GET: Event/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eventId,eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
        {
            if (ModelState.IsValid)
            {
                db.EventDataModels.Add(eventDataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventDataModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Event/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDataModel eventDataModel = db.EventDataModels.Find(id);
            if (eventDataModel == null)
            {
                return HttpNotFound();
            }
            return View(eventDataModel);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eventId,eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventDataModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventDataModel);
        }

        [Authorize(Roles = "Admin")]
        // GET: Event/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDataModel eventDataModel = db.EventDataModels.Find(id);
            if (eventDataModel == null)
            {
                return HttpNotFound();
            }
            return View(eventDataModel);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EventDataModel eventDataModel = db.EventDataModels.Find(id);
            db.EventDataModels.Remove(eventDataModel);
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

        //Volunte
        public ActionResult NotRealVolunteer(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDataModel eventDataModel = db.EventDataModels.Find(id);
            eventDataModel.volunteersNeeded--;
            return View(eventDataModel);
        }

        // POST: Event/NotRealVolunteer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NotRealVolunteer([Bind(Include = "eventId,eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventDataModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventDataModel);
        }
    }
}
