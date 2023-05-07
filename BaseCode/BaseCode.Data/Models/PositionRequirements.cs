using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class PositionRequirements
    {
        [Key]
        [Column("PositionRequirementsID")]
        public int PositionRequirementsID { get; set; }

        [Column("PositionID")]
        public int PositionID { get; set; }

        [Column("Description")]
        public string Description { get; set; }


        // Foreign Keys
        [ForeignKey("PositionID")]
        [JsonIgnore]
        public virtual Position Position { get; set; }

        [JsonIgnore]
        public virtual ICollection<PositionRequirements> PositionRequirement { get; set; }
    }
}
