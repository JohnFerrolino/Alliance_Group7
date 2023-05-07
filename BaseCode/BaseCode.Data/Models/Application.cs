using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Application
    {
        [Key]
        [Column("ApplicationID")]
        public int ApplicationID { get; set; }

        [Column("ApplicationCode", TypeName = "varchar(250)")]
        public string ApplicationCode { get; set; }

        [Column("PositionID")]
        public int PositionID { get; set; }

        [Column("ApplicantID")]
        public int ApplicantID { get; set; }

        [Column("StatusID")]
        public int StatusID { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [ForeignKey("PositionID")]
        [JsonIgnore]
        public virtual Position Position { get; set; }

        [ForeignKey("ApplicantID")]
        [JsonIgnore]
        public virtual Applicant Applicant { get; set; }

        [ForeignKey("StatusID")]
        [JsonIgnore]
        public virtual Status Status { get; set; }
    }
}
