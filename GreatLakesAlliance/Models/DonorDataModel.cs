using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GreatLakesAlliance.Models
{
    public class DonorDataModel
    {
        public string fullName { get; set; }
        public long cardNum { get; set; }
        public string expDate { get; set; }
        public int ccv { get; set; }
        public int amount { get; set; }

    }
}