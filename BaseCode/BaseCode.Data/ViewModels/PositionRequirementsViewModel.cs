using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class PositionRequirementsViewModel
    {
        [JsonProperty("positionrequirements_ID")]

        public int PositionRequirementsID { get; set; }

        [JsonProperty("positionrequirements_positionID")]
        public int PositionID { get; set; }

        [JsonProperty("positionrequirements_description")]
        public string Description { get; set; }
    }
}
