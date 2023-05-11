using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class ApplicantSearchViewModel
    {
        [JsonProperty("applicantid")]
        public string ApplicantID { get; set; }

        [JsonProperty("applicantfirstname")]
        public string ApplicantFirstname { get; set; }

        [JsonProperty("applicantlastname")]
        public string ApplicantLastname { get; set; }

        [JsonProperty("applicantemail")]
        public string ApplicantEmail { get; set; }

        [JsonProperty("applicantphonenumber")]
        public string ApplicantPhoneNumber { get; set; }

        [JsonProperty("applicantresume")]
        public string ApplicantResume { get; set; }

        [JsonProperty("applicantposition")]
        public string ApplicantPosition { get; set; }

        [JsonProperty("applicantstatus")]
        public string ApplicantStatus { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("sortBy")]
        public string SortBy { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }
    }
}
