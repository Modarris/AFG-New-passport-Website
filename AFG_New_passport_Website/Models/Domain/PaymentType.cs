using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Primitives;

namespace AFG_New_passport_Website.Models.Domain
{
    public class PaymentType
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public String Type { get; set; }
    }
}
