﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseCode.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public string EnrollYear { get; set; }
        public string Subject { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Column("StudentName", TypeName = "varchar(100)")]
        public string StudentName { get; set; }
    }
}
