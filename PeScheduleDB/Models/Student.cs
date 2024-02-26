namespace PeScheduleDB.Models
{
    public class Student
    {
        public int Student_ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Emergency_Contact { get; set; }
        public int Class_ID { get; set; }

    }
}
