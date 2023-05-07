using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Status
    {
        [Key]
        [Column("StatusID")]
        public int StatusID { get; set; }

        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }
    }
}
