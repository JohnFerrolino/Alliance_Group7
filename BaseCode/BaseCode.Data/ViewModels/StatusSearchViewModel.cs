using Newtonsoft.Json;

namespace BaseCode.Data.ViewModels.Common
{
    public class StatusSearchViewModel
    {
        [JsonProperty("statusid")]
        public string StatusID { get; set; }

        [JsonProperty("statusname")]
        public string StatusName { get; set; }

        [JsonProperty("statusisactive")]
        public string StatusIsActive { get; set; }
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
