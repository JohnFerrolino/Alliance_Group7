using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class PositionViewModel
    {
        [JsonProperty("position_id")]
        public int PositionID { get; set; }

        [JsonProperty("position_name")]
        [Required(ErrorMessage = "Name is Required.")]
        public string Name { get; set; }

        [JsonProperty("position_requirements")]
        public List<string> PositionRequirements { get; set; }

        [JsonProperty("position_descriptions")]
        public string Description { get; set; }

    }
}
