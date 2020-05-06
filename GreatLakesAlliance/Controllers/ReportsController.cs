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

            //adds all volunteers to the model 'realList'
            //which holds specific information for the reports
            foreach (var item in volunteers)
            {
                VolunteerModel tempVol = new VolunteerModel();

                tempVol.userId = item.UserId;
                tempVol.eventId = item.EventId;
                tempVol.fullName = item.FullName;

                string eName = db.EventDataModels.Where(a => a.eventId == item.EventId).Select(a => a.eventName).First();
                tempVol.eventName = eName;
                
                //adds temp volunteer to full list of volunteers
                realList.Add(tempVol);
            }

            //reorders the list to sort by event volunteered at
            realList = realList.OrderBy(a => a.eventName).ToList();

            return View(realList);
        }

        public ActionResult Donations()
        {
            List<DonorDataModel> donators = db.DonorDataModels.OrderBy(a => a.fullName).ToList();

            List<DonorModel> realDonors = new List<DonorModel>();

            //goes through all donations in 'donators' and adds them to 'realDonors'
            foreach (var item in donators)
            {
                DonorModel tempDonor = new DonorModel();

                tempDonor.eventId = item.eventId;
                tempDonor.amount = item.amount;
                tempDonor.fullName = item.fullName;
                tempDonor.userId = item.userId;

                //checks if the donation was made to the GLA or not
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

            //puts all users in the 'realUsers' list
            foreach (var item in users)
            {
                UserModel tempUser = new UserModel();
                tempUser.FullName = item.FullName;
                tempUser.Email = item.Email;
                tempUser.Id = item.Id;
                tempUser.Deleted = item.Deleted;

                realUsers.Add(tempUser);
            }

            return View(realUsers);
        }

        public ActionResult UserData(string userId)
        {
            UserData userData = new UserData();

            //lists of volunteers and donations from db
            List<VolunteeredEventsModel> volunteered = db.VolunteeredEventsModel.Where(a => a.UserId == userId).ToList();
            List<DonorDataModel> donations = db.DonorDataModels.Where(a => a.userId == userId).ToList();

            //helper lists that will be added to 'userData'
            List<ShortVolunteer> allVol = new List<ShortVolunteer>();
            List<ShortDonation> allDonations = new List<ShortDonation>();

            //adding items to the 'allVol' list
            foreach (var item in volunteered)
            {
                ShortVolunteer temp = new ShortVolunteer();

                string eName = db.EventDataModels.Where(a => a.eventId == item.EventId).Select(a => a.eventName).First();
                temp.eventName = eName;

                allVol.Add(temp);

            }

            //adding items to the 'allDonations' list
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

            //adds the 2 lists the the 'userData' model
            userData.donations = allDonations;
            userData.volunteer = allVol;

            ViewBag.name = db.Users.Where(a => a.Id == userId).Select(a => a.FullName).First();

            return View(userData);
        }

        public ActionResult DeleteAccount(string userId)
        {
            //grabs user account and sets the deleted bool to true
            ApplicationUser user = db.Users.Where(a => a.Id == userId).First();
            user.Deleted = true;

            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}