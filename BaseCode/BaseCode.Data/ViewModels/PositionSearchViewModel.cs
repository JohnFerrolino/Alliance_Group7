using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class PositionSearchViewModel
    {
        [JsonProperty("positionid")]
        public string PositionID { get; set; }

        [JsonProperty("positionname")]
        public string PositionName { get; set; }

        [JsonProperty("positionrequirements")]
        public string PositionRequirements { get; set; }

        [JsonProperty("positiondescription")]
        public string PositionDescription { get; set; }

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
