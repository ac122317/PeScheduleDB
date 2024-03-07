namespace PeScheduleDB.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public List<Course>Courses { get; set; }
    }
}
