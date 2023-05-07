using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Position
    {
        [Key]
        [Column("PositionID")]
        public int PositionID { get; set; }

        [Column("PosititionName")]
        public string Name { get; set; }

        [Column("Requirements")]
        public virtual ICollection<PositionRequirements> PositionRequirements { get; set; }

        [Column("Description")]
        public string Description { get; set; }
    }
}
