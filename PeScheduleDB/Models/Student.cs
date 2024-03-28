﻿using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(60)]
        public string Email { get; set; }

        [Required, MaxLength(25)]
        public string Emergency_Contact { get; set; }
        public List<Course> Courses { get; set; }
    }
}
