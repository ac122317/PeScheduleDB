using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeScheduleDB.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Courses { get; set; }

        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public Location Locations { get; set; }
        public DateTime Date { get; set; }
    }
}
