using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Applicant
    {
        [Key]
        [Column("ApplicantID")]
        public int ApplicantID { get; set; }

        [Column("FirstName", TypeName = "varchar(250)")]
        public string FirstName { get; set; }

        [Column("LastName", TypeName = "varchar(250)")]
        public string LastName { get; set; }

        [Column("EmailAddress")]
        public string EmailAddress { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("PositionID")]
        public int PositionID { get; set; }

        [ForeignKey("PositionID")]
        [JsonIgnore]
        public virtual Position Position { get; set; }
    }
}
