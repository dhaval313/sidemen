using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication3.Models
{
    public class LOC_ContactModel
    {
        public int ContactID { get; set; }
        [Required(ErrorMessage = "Enter Contact Name")]
        [DisplayName("Contact")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Please do not enter values over 50 characters and less than 3")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Select Country")]
        [DisplayName("City")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Select State")]
        [DisplayName("City")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Select City")]
        [DisplayName("City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "Enter Pincode")]
        [DisplayName("City")]

        public int Pincode { get; set; }
        [Required(ErrorMessage = "Enter MobileNumber")]
        [DisplayName("City")]
        [StringLength(20, MinimumLength = 3)]
        public string MobileNumber { get; set; }
        public string? AlternativeMobile { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [DisplayName("City")]
        [StringLength(20, MinimumLength = 3)]
        public string Email { get; set; }
        
        public DateTime BirthDate { get; set; }
        public DateTime AnniversaryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string? LinkedIn { get; set; }
        public string? Twitter { get; set; }
        public string? Insta { get; set; }
        public string TypeOfProfession { get; set; }
        public string? CompanyName { get; set; }
        public string? Designation { get; set; }
        

        public int ContactCategoryID { get; set; }

    }
}
