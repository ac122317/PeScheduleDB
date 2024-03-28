using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeScheduleDB.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [ForeignKey("Course"), Required]
        public int CourseId { get; set; }
        public Course Courses { get; set; }

        [ForeignKey("Location"), Required]
        public int LocationId { get; set; }
        public Location Locations { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
