using GreatLakesAlliance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreatLakesAlliance.Controllers
{
    public class DonorController : Controller
    {
        // GET: Donor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Donor/PrevDonations/5
        public ActionResult PrevDonations()   //add (int d) for id for the person donating to grab their previous donations
        {
            var prevDonations = new List<DonorDataModel>
            {
                new DonorDataModel() { amount = 100, cardNum = 1111111111111111, ccv = 111, expDate = "12/20", fullName = "John Johnson" },
                new DonorDataModel() { amount = 345, cardNum = 9348759384579873, ccv = 273, expDate = "04/20", fullName = "John Johnson" },
                new DonorDataModel() { amount = 100, cardNum = 1111111111111111, ccv = 111, expDate = "12/20", fullName = "John Johnson" }

            };
            return View(prevDonations);
        }

        // GET: Donor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
