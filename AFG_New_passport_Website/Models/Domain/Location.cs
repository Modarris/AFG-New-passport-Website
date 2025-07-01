using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AFG_New_passport_Website.Models.Domain
{
    public class Location
    {
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string NameLocal { get; set; } //


        [Required]
        [StringLength(50)]
        public string Name { get; set; } // 

        public bool IsActive { get; set; } = true;

       /* public int CountryId { get; set; }
        public Country Country { get; set; }*/
    /*    public ICollection<Profile> BirthProfiles { get; set; }
        public ICollection<Profile> ProcessingProfiles { get; set; }*/
    


        // 
        [DefaultValue(LocationUsageType.Both)]
        public LocationUsageType UsageType { get; set; } = LocationUsageType.Both;

        //  (enum)
        public enum LocationUsageType
        {
            Birth = 1,       // Just for birth location
            Processing = 2,  // Just for processing location documents
            Both = 3         // both of
        }

    }
}
