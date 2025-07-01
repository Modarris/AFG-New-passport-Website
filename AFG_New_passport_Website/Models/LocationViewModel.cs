using AFG_New_passport_Website.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AFG_New_passport_Website.Models
{
    public class LocationViewModel
    {
        public Location Location { get; set; }
        /*public List<Country> Countries { get; set; }*/
    }
}
