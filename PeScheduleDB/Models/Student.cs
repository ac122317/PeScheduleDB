namespace PeScheduleDB.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Emergency_Contact { get; set; }
        public List<Course> Courses { get; set; }
    }
}
