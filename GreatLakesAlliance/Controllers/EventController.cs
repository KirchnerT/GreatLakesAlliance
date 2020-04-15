using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GreatLakesAlliance.Models;
using Microsoft.AspNet.Identity;

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
        public ActionResult Details(int id)
        {
            if (id == 0)
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
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
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
        public ActionResult Edit(int id)
        {
            if (id == 0)
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
        public ActionResult Edit([Bind(Include = "eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
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
        public ActionResult Delete(int id)
        {
            if (id == 0)
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
        public ActionResult DeleteConfirmed(int id)
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
        public ActionResult NotRealVolunteer(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var userId = User.Identity.GetUserId();
            var chkIfVolunteered = db.VolunteeredEventsModel.Where(a => a.EventId == id && a.UserId == userId)
                                        .Select(a => a.Id).FirstOrDefault();

            EventDataModel eventDataModel = db.EventDataModels.Find(id);

            if(chkIfVolunteered != 0)
            {
                //they are volunteered for this event
                ViewData["Message"] = "CannotVolunteer";
            }
            else if(chkIfVolunteered == 0 && eventDataModel.volunteersNeeded > 0)
            {
                //they are not volunteeered for this event and the event still needs volunteers
                ViewData["Message"] = "CanVolunteer";
            }

            return View(eventDataModel);
        }

        // POST: Event/NotRealVolunteer/5
        [HttpPost, ActionName("NotRealVolunteer")]
        [ValidateAntiForgeryToken]
        public ActionResult VolunteerOrCancel(int id, [Bind(Include = "UserId, EventId")] VolunteeredEventsModel volunteersNeeded)
        {
            if( Request.Form["Volunteer"] != null)
            {
                EventDataModel eventDataModel = db.EventDataModels.Find(id);
                eventDataModel.volunteersNeeded = eventDataModel.volunteersNeeded - 1;

                VolunteeredEventsModel volunteer = new VolunteeredEventsModel();
                volunteer.UserId = User.Identity.GetUserId();
                volunteer.EventId = id;
                db.VolunteeredEventsModel.Add(volunteer);

                db.SaveChanges();
            }
            else if (Request.Form["CancelVolunteer"] != null)
            {
                EventDataModel eventDataModel = db.EventDataModels.Find(id);
                eventDataModel.volunteersNeeded = eventDataModel.volunteersNeeded + 1;
                db.SaveChanges();

                var userId = User.Identity.GetUserId();
                var SQLVolunteerAndEvent = db.VolunteeredEventsModel.Where(a => a.EventId == id && a.UserId == userId)
                                        .Select(a => a.Id).FirstOrDefault();

                VolunteeredEventsModel volunteerAndEvents = db.VolunteeredEventsModel.Find(SQLVolunteerAndEvent);

                db.VolunteeredEventsModel.Remove(volunteerAndEvents);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
