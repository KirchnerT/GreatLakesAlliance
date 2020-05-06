using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreatLakesAlliance.Controllers
{
    public class HelpController : Controller
    {
        // GET: Help
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventDetails()
        {
            return View();
        }

        public ActionResult EventVolunteer()
        {
            return View();
        }

        public ActionResult EventDonate()
        {
            return View();
        }

        public ActionResult AdminRoles()
        {
            return View();
        }

        public ActionResult AdminDelete()
        {
            return View();
        }

        public ActionResult AdminReports()
        {
            return View();
        }
    }
}