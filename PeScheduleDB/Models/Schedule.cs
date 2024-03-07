namespace PeScheduleDB.Models
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public List<Course>Courses { get; set; }
        public List<Location> Locations { get; set; }
        public DateTime Date { get; set; }
    }
}
