using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreatLakesAlliance.Models
{        
    public class UserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

    }

    public class VolunteerModel
    {
        public string userId { get; set; }
        public int eventId { get; set; }
        public string fullName { get; set; }
        public string eventName { get; set; }
    }

    public class DonorModel
    {
        public string userId { get; set; }
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string fullName { get; set; }
        public int amount { get; set; }
    }

    public class UserData
    {
        public List<ShortDonation> donations { get; set; }
        public List<ShortVolunteer> volunteer { get; set; }
    }

    public class ShortDonation
    {
        public string eventName { get; set; }
        public int amount { get; set; }
        public string cardNumber { get; set; }
    }

    public class ShortVolunteer
    {
        public string eventName { get; set; }
    }
}