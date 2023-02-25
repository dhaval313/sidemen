using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models

{
	public class LOC_CountryModel
	{

		public int CountryID { get; set; }

		[Required(ErrorMessage = "Select Country Name")]
		[DisplayName("Country")]
		[StringLength(20,MinimumLength =3)]
		public string CountryName { get; set; }
        [Required(ErrorMessage = "Select Country Code")]
        [DisplayName("Code")]
        [StringLength(20, MinimumLength = 1)]
        public string CountryCode { get; set; }
		public DateTime? CreationDate { get; set; }
		public DateTime? ModificationDate { get; set;}
      
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
    }
	public class LOC_CountryDropDownModel
	{
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
