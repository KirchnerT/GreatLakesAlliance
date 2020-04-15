using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GreatLakesAlliance.Models
{
    [Table("AspNetDonations")]
    public class DonorDataModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }
        public string cardNumber { get; set; }
        public string expirationDate { get; set; }
        public string ccv { get; set; }
        public int amount { get; set; }
        public string orgEvent { get; set; }

    }
}