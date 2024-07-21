using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeScheduleDB.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        //This section represents course as a foreign key for this specific table, the course must have a value as per the required validation attribute
        [ForeignKey("Course"), Required]
        public int CourseId { get; set; }
        public Course Courses { get; set; }

        //This section represents location as a foreign key for this specific table, is a required field.
        [ForeignKey("Location"), Required]
        public int LocationId { get; set; }
        public Location Locations { get; set; }

        //Date and time of the schedule entry, is also required.
        [Required]
        public DateTime Date { get; set; }
    }
}
