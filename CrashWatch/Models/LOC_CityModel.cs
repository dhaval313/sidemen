using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace  WebApplication3.Models
{
    public class LOC_CityModel
    {
        
        public int CityID { get; set; }
        [Required(ErrorMessage = "Select City Name")]
        [DisplayName("City")]
        [StringLength(20, MinimumLength = 3)]
        public string CityName { get; set; }
        [Required(ErrorMessage = "Select Country")]
        [DisplayName("State")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Select State")]
        [DisplayName("State")]
        public int StateID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
