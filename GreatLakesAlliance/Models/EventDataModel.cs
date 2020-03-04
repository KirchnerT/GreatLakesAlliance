using System;
using System.ComponentModel.DataAnnotations;

namespace GreatLakesAlliance.Models
{
    public class EventDataModel
    {
        [Key]
        public string eventId { get; set; }
        public string eventName { get; set; }
        public DateTime eventStarDate { get; set; }
        public DateTime eventStartEnd { get; set; }
        public int volunteersNeeded { get; set; }
        public string location { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string description { get; set; }

    }
}