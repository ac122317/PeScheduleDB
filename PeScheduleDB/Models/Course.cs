namespace PeScheduleDB.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<Location> Locations { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Student>Students { get; set; } 
       
    }
}