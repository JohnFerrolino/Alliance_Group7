using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseCode.Data.ViewModels
{
    public class StatusViewModel
    {
        [JsonProperty("status_id")]
        public int StatusID { get; set; }

        [JsonProperty("status_name")]
        public string Name { get; set; }

        [JsonProperty("status_isactive")]
        public bool IsActive { get; set; }
    }
}
