using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class ApplicationViewModel
    {
        [JsonProperty("application_id")]
        public int ApplicationID { get; set; }

        [JsonProperty("application_applicant")]
        public int ApplicantID { get; set; }

        [JsonProperty("applicaton_code")]
        public string ApplicationCode { get; set; }

        [JsonProperty("application_position")]
        public int PositionID { get; set; }

        [JsonProperty("application_status")]
        public int StatusID { get; set; }

        [JsonProperty("application_isactive")]
        public bool IsActive { get; set; }

    }
}
