using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreatLakesAlliance.Models
{        
    [Table("dbo.AspNetEventData")]
    public class EventDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string eventStartDate { get; set; }
        public string eventEndDate { get; set; }
        public int volunteersNeeded { get; set; }
        public string location { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string description { get; set; }

    }

    [Table("dbo.AspNetVolunteeredEvents")]
    public class VolunteeredEventsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int EventId { get; set; }
    }
}