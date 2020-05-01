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
            if(eventId == 0)
            {
                ViewData["Message"] = "0";
            }
            else
            {
                ViewData["Message"] = eventId + "";
            }

            ViewBag.Events = new SelectList(db.EventDataModels, "eventId", "eventName", eventId);

            return View();
        }


        // POST: Donation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cardNumber,expirationDate,ccv,amount,eventId,userId,fullName")] DonorDataModel donorDataModel, string eventId)
        {
            if (ModelState.IsValid)
            {
                int eventIdentification = 0;
                //int eventIdentification = Int32.Parse(eventId);
                try
                {
                    eventIdentification = Int32.Parse(Request.Form["Events"].ToString());
                }
                catch (SystemException e)
                {

                }

                donorDataModel.eventId = eventIdentification;
                donorDataModel.fullName = User.Identity.Name;
                donorDataModel.userId = User.Identity.GetUserId();
                db.DonorDataModels.Add(donorDataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donorDataModel);
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
