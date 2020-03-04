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
    [Authorize(Roles ="Admin, User, Volunteer, Donor")]
    public class DonationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Donation
        public ActionResult Index()
        {
            return View(db.DonorDataModels.ToList());
        }

        // GET: Donation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDataModel donorDataModel = db.DonorDataModels.Find(id);
            if (donorDataModel == null)
            {
                return HttpNotFound();
            }
            return View(donorDataModel);
        }

        // GET: Donation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cardNumber,expirationDate,ccv,amount,orgEvent")] DonorDataModel donorDataModel)
        {
            if (ModelState.IsValid)
            {
                db.DonorDataModels.Add(donorDataModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donorDataModel);
        }

        // GET: Donation/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDataModel donorDataModel = db.DonorDataModels.Find(id);
            if (donorDataModel == null)
            {
                return HttpNotFound();
            }
            return View(donorDataModel);
        }

        // POST: Donation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cardNumber,expirationDate,ccv,amount,orgEvent")] DonorDataModel donorDataModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donorDataModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donorDataModel);
        }

        // GET: Donation/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonorDataModel donorDataModel = db.DonorDataModels.Find(id);
            if (donorDataModel == null)
            {
                return HttpNotFound();
            }
            return View(donorDataModel);
        }

        // POST: Donation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DonorDataModel donorDataModel = db.DonorDataModels.Find(id);
            db.DonorDataModels.Remove(donorDataModel);
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
