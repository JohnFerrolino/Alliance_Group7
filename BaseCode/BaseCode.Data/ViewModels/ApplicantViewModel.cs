using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels.Common
{
    public class ApplicantViewModel
    {
        [JsonProperty("applicant_id")]
        public int ApplicantID { get; set; }

        [JsonProperty("applicant_firstname")]
        [Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }

        [JsonProperty("applicant_lastname")]
        [Required(ErrorMessage = "Last Name is Required.")]
        public string LastName { get; set; }

        [JsonProperty("applicant_email")]
        [Required(ErrorMessage = "Email is Required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string EmailAddress { get; set; }

        [JsonProperty("applicant_resume")]
        public string Resume { get; set; }

        [JsonProperty("applicant_phonenumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("applicant_status")]
        public int StatusID { get; set; }

        [JsonProperty("applicant_position")]
        public int PositionID { get; set; }
    }
}
