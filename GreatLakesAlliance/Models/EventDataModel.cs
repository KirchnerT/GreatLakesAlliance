using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreatLakesAlliance.Models
{
    public class EventDataModel
    {
        public string eventName { get; set; }
        public DateTime eventStartDateTime { get; set; }
        public int volunteersNeeded { get; set; }

    }
}