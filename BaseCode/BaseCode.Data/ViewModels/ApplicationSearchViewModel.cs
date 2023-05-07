using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class ApplicationSearchViewModel
    {
        [JsonProperty("applicationid")]
        public string ApplicationId { get; set; }

        [JsonProperty("applicationapplicant")]
        public string ApplicationApplicant { get; set; }

        [JsonProperty("applicationCode")]
        public string ApplicationCode { get; set; }

        [JsonProperty("applicationposition")]
        public string ApplicationPosition { get; set; }

        [JsonProperty("applicationstatus")]
        public string ApplicationStatus { get; set; }

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
