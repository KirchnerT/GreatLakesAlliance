using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GreatLakesAlliance.Models;
using System.Collections.Generic;

namespace GreatLakesAlliance.Controllers
{
    public class EventController : Controller
    {
        
        // GET: Event
        public ActionResult Index()
        {
            //this is where we would grab database values (hardcoding for now)
            var eventList = new List<EventDataModel>
            {
                new EventDataModel() { eventName = "Event1", eventStartDateTime = DateTime.Now, volunteersNeeded = 2},
                new EventDataModel() { eventName = "Event2", eventStartDateTime = DateTime.Now, volunteersNeeded = 17},
                new EventDataModel() { eventName = "Event3", eventStartDateTime = DateTime.Now, volunteersNeeded = 22},
                new EventDataModel() { eventName = "Event4", eventStartDateTime = DateTime.Now, volunteersNeeded = 1}
            };
            return View(eventList);
        }

        public ActionResult Edit(string eventName)
        {
            var id = Request.QueryString["id"];

            //retrive data from database
            return View();
        }

        [HttpPost]
        public ActionResult Edit()
        {
            var name = Request["eventName"];
            var vNeeded = Request["volunteersNeeded"];
            var startDate = Request["eventStartDateTime"];

            //update database here...

            return RedirectToAction("Index");
        }


    }
}