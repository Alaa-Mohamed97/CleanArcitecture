﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArcitecture.Domain.Entities
{
    public class StudentSubject
    {
        [Key]
        public int StudSubID { get; set; }
        public int StudID { get; set; }
        public int SubID { get; set; }

        [ForeignKey("StudID")]
        public virtual Student Student { get; set; }

        [ForeignKey("SubID")]
        public virtual Subjects Subject { get; set; }
    }
}
