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
            String dateNow = DateTime.Now.ToString("MM/dd/yyyy");
            
            //sends a list of all active/future events to the view
            return View(db.EventDataModels.Where(a => a.eventEndDate.CompareTo(dateNow) >= 0).OrderBy(a => a.eventStartDate).ToList());
        }

        // GET: Event/Details/5
        public ActionResult Details(int id)
        {
            //grabbing event details, if an event is found
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
        {
            if (ModelState.IsValid)
            {
                eventDataModel.eventEndDate = eventDataModel.eventStartDate.Substring(13, 10);
                eventDataModel.eventStartDate = eventDataModel.eventStartDate.Substring(0, 10);

                //sets a time if no time was selected in the create form
                if(eventDataModel.startTime == null)
                {
                    eventDataModel.startTime = "12:00am";
                }

                if (eventDataModel.endTime == null)
                {
                    eventDataModel.endTime = "11:59pm";
                }

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
        public ActionResult Edit([Bind(Include = "eventId,eventName,eventStartDate,eventEndDate,volunteersNeeded,location,startTime,endTime,description")] EventDataModel eventDataModel)
        {
            if (ModelState.IsValid)
            {
                eventDataModel.eventEndDate = eventDataModel.eventStartDate.Substring(13, 10);
                eventDataModel.eventStartDate = eventDataModel.eventStartDate.Substring(0, 10);

                //sets a time if no time was selected in the create form
                if (eventDataModel.startTime == null)
                {
                    eventDataModel.startTime = "12:00am";
                }

                if (eventDataModel.endTime == null)
                {
                    eventDataModel.endTime = "11:59pm";
                }

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
            //finds the event in the database and deletes it 

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

        //Volunteer/Unvolunteer method
        public ActionResult NotRealVolunteer(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //checks if the user has volunteered or not to this event
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
        [Authorize(Roles = "Admin,Donor,Volunteer,User")]
        [HttpPost, ActionName("NotRealVolunteer")]
        [ValidateAntiForgeryToken]
        public ActionResult VolunteerOrCancel(int id, [Bind(Include = "UserId, EventId")] VolunteeredEventsModel volunteersNeeded)
        {
          
            //need to check if user is a 'user'.
            //if so, change role to volunteer

            if( Request.Form["Volunteer"] != null)
            {
                string userId = User.Identity.GetUserId();

                var SQLVolunteerAndEventsList = db.VolunteeredEventsModel.Where(a => a.UserId == userId)
                                               .Select(a => a.EventId).ToList();

                //EventDataModel databaseEvent;
                EventDataModel eventDataModel = db.EventDataModels.Find(id);

                for (int i = 0; i < SQLVolunteerAndEventsList.Count; i++) 
                {
                    EventDataModel databaseEvent = db.EventDataModels.Find(SQLVolunteerAndEventsList[i]);

                    //check if the date ranges don't overlap
                    if (eventDataModel.eventStartDate.CompareTo(databaseEvent.eventEndDate) > 0)
                    {
                        //they do not overlap
                    }
                    else if (eventDataModel.eventEndDate.CompareTo(databaseEvent.eventStartDate) < 0)
                    {
                        //they do not overlap
                    }
                    else
                    {
                        //the dates overlap somewhere 
                        //check if the ends of the dates just touch

                        if (eventDataModel.eventStartDate.Equals(databaseEvent.eventEndDate))
                        {
                            DateTime dbEnd = DateTime.Parse(databaseEvent.endTime);
                            DateTime eventStart = DateTime.Parse(eventDataModel.startTime);

                            if (DateTime.Compare(dbEnd, eventStart) > 0)
                            {
                                return RedirectToAction("NotRealVolunteer");
                            }
                            
                        }
                        else if (eventDataModel.eventEndDate.Equals(databaseEvent.eventStartDate))
                        {
                            DateTime dbStart = DateTime.Parse(databaseEvent.startTime);
                            DateTime eventEnd = DateTime.Parse(eventDataModel.endTime);

                            if (DateTime.Compare(dbStart, eventEnd) < 0)
                            {
                                return RedirectToAction("NotRealVolunteer");
                            }
                        }
                    }
                }

                //if this far
                //the users other volunteer times do not overlap so they can volunteer

                eventDataModel.volunteersNeeded = eventDataModel.volunteersNeeded - 1;

                VolunteeredEventsModel volunteer = new VolunteeredEventsModel();
                volunteer.FullName = User.Identity.Name;
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
