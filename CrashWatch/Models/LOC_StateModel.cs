using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication3.Models
{
    public class LOC_StateModel
    {
        
        public int StateID { get; set; }
        [Required(ErrorMessage = "Select State Name")]
        [DisplayName("State")]
        [StringLength(20, MinimumLength = 3)]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Select StateCode")]
        [DisplayName("State")]
        [StringLength(20, MinimumLength = 1)]
        public string StateCode { get; set; }
        [Required(ErrorMessage = "Select Country")]
        [DisplayName("State")]
        public int CountryID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
