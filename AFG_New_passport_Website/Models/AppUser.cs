using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AFG_New_passport_Website.Models
{
    public class AppUser: IdentityUser
    {
        [Required]
        [StringLength(100)]

        public string Name { get; set; }
    }
}
