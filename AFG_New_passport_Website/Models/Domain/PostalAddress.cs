using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AFG_New_passport_Website.Models.Domain
{
    public class PostalAddress
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;

        public int CityId { get; set; }
        public City City { get; set; }



    }

}
