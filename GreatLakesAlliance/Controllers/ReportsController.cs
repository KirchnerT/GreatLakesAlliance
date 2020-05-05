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
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Reports
        public ActionResult Index()
        {
            ViewBag.Users = new SelectList(db.Users, "Id", "FullName");

            return View();
        }

        public ActionResult GetReport()
        {
            return RedirectToAction("Index");

        }

        public ActionResult Volunteers()
        {
            List<VolunteeredEventsModel> volunteers = db.VolunteeredEventsModel.ToList();

            List<VolunteerModel> realList = new List<VolunteerModel>();

            foreach (var item in volunteers)
            {
                VolunteerModel tempVol = new VolunteerModel();

                tempVol.userId = item.UserId;
                tempVol.eventId = item.EventId;
                tempVol.fullName = item.FullName;

                string eName = db.EventDataModels.Where(a => a.eventId == item.EventId).Select(a => a.eventName).First();
                tempVol.eventName = eName;
                

                realList.Add(tempVol);
            }

            realList = realList.OrderBy(a => a.eventName).ToList();

            return View(realList);
        }

        public ActionResult Donations()
        {
            List<DonorDataModel> donators = db.DonorDataModels.OrderBy(a => a.fullName).ToList();

            List<DonorModel> realDonors = new List<DonorModel>();

            foreach (var item in donators)
            {
                DonorModel tempDonor = new DonorModel();

                tempDonor.eventId = item.eventId;
                tempDonor.amount = item.amount;
                tempDonor.fullName = item.fullName;
                tempDonor.userId = item.userId;

                string eName = "Great Lakes Alliance";
                if (!(item.eventId == 0))
                {
                    eName = db.EventDataModels.Where(a => a.eventId == item.eventId).Select(a => a.eventName).First(); tempDonor.eventName = eName;
                }

                tempDonor.eventName = eName;

                realDonors.Add(tempDonor);
            }

            realDonors = realDonors.OrderBy(a => a.eventName).ToList();

            return View(realDonors);
        }

        public ActionResult Users()
        {

            List<ApplicationUser> users = db.Users.OrderBy(a => a.FullName).ToList();
            List<UserModel> realUsers = new List<UserModel>();

            foreach (var item in users)
            {
                UserModel tempUser = new UserModel();
                tempUser.FullName = item.FullName;
                tempUser.Email = item.Email;
                tempUser.Id = item.Id;

                realUsers.Add(tempUser);
            }

            return View(realUsers);
        }

        public ActionResult UserData(string userId)
        {
            UserData userData = new UserData();

            List<VolunteeredEventsModel> volunteered = db.VolunteeredEventsModel.Where(a => a.UserId == userId).ToList();
            List<DonorDataModel> donations = db.DonorDataModels.Where(a => a.userId == userId).ToList();

            List<ShortVolunteer> allVol = new List<ShortVolunteer>();
            List<ShortDonation> allDonations = new List<ShortDonation>();

            foreach (var item in volunteered)
            {
                ShortVolunteer temp = new ShortVolunteer();

                string eName = db.EventDataModels.Where(a => a.eventId == item.EventId).Select(a => a.eventName).First();
                temp.eventName = eName;

                allVol.Add(temp);

            }

            foreach (var item in donations)
            {
                ShortDonation temp = new ShortDonation();

                temp.amount = item.amount;
                temp.cardNumber = item.cardNumber;

                string eName = "Great Lakes Alliance";
                if (!(item.eventId == 0))
                {
                    eName = db.EventDataModels.Where(a => a.eventId == item.eventId).Select(a => a.eventName).First();
                }
                temp.eventName = eName;

                allDonations.Add(temp);
            }

            userData.donations = allDonations;
            userData.volunteer = allVol;

            ViewBag.name = db.Users.Where(a => a.Id == userId).Select(a => a.FullName).First();

            return View(userData);
        }
    }
}