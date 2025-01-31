﻿using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GreatLakesAlliance.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name ="Full Name"), Required]        
        [StringLength(100)]
        public string FullName { get; set; }
        public bool Deleted { get; set; }
        //public bool Gender { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        object placeHolderVariable;
        public System.Data.Entity.DbSet<GreatLakesAlliance.Models.EventDataModel> EventDataModels { get; set; }
        public System.Data.Entity.DbSet<GreatLakesAlliance.Models.VolunteeredEventsModel> VolunteeredEventsModel { get; set; }

        public System.Data.Entity.DbSet<GreatLakesAlliance.Models.DonorDataModel> DonorDataModels { get; set; }
    }
}