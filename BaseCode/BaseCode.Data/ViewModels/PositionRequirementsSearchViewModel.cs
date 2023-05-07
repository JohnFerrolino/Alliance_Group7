using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class PositionRequirementsSearchViewModel
    {
        [JsonProperty("positionrequirementsid")]
        public int PositionRequirementsID { get; set; }

        [JsonProperty("positionrequirementspositionid")]
        public int PositionRequirementsPositionID { get; set; }

        [JsonProperty("positionrequirementsdescription")]
        public string PositionRequirementsDescription { get; set; }

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
