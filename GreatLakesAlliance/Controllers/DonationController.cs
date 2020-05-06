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
    [Authorize(Roles ="Admin, User, Volunteer, Donor")]
    public class DonationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donation
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            return View(db.DonorDataModels.Where(a => a.userId == userId).ToList());
        }      

        // GET: Donation/Create
        [HttpGet]
        public ActionResult Create(int eventId)
        {
            //checks if there is no event selected
            //if 0, then set the selected donation location to GLA
            if(eventId == 0)
            {
                ViewData["Message"] = "0";
            }
            else
            {
                ViewData["Message"] = eventId + "";
            }

            String dateNow = DateTime.Now.ToString("MM/dd/yyyy");       

            //Grabs a list of all events that are going on or 
            //have yet to start
            var activeEventId = db.EventDataModels.Where(a => a.eventEndDate.CompareTo(dateNow) >= 0)
                                        .Select(a => a.eventId).ToList();

            List<EventDataModel> e = new List<EventDataModel>();

            //adds a list of all the events into a model to go into the view
            foreach (int item in activeEventId)
            {                
                e.Add(db.EventDataModels.Find(item));
            }
                                            
            ViewBag.Events = new SelectList(e, "eventId", "eventName", eventId);  
            return View();
        }


        // POST: Donation/Create     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cardNumber,expirationDate,ccv,amount,eventId,userId,fullName")] DonorDataModel donorDataModel, string eventId)
        {
            if (ModelState.IsValid)
            {
                int eventIdentification = 0;
                try
                {
                    eventIdentification = Int32.Parse(Request.Form["Events"].ToString());
                }
                catch (SystemException e)
                {

                }

                //changes all information into a model to go into the database
                donorDataModel.eventId = eventIdentification;
                donorDataModel.fullName = User.Identity.Name;
                donorDataModel.userId = User.Identity.GetUserId();
                db.DonorDataModels.Add(donorDataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donorDataModel);
        }      

        //a cleanup method
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
