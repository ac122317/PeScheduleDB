using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Student
    {
        //Unique identifier for each student
        [Key]
        public int StudentId { get; set; }

        //Student Last name field with several validation rules, an error message will display if at least one validation rule is not complied with (e.g., as per the regular expression the name must only have letters in it).
        [Required, MinLength(2), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must only contain letters, no special characters or spaces.")]
        public string LastName { get; set; }

        //Student First name field with several validation rules, an error message will display if at least one validation rule is not complied with.
        [Required, MinLength(2), MaxLength(20), RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name must only contain letters, no special characters or spaces.")]
        public string FirstName { get; set; }

        //Student Email address field with several validation rules, an error message will display if at least one validation rule is not complied with (e.g., not in a correct email address format).
        [Required, MaxLength(35), EmailAddress]
        public string Email { get; set; }

        //Student Year Level field with a couple of validation rules, an error message will display if the year level is not within the specified range (9-13)
        [Required, Range(9,13)]
        public int YearLevel { get; set; }

        //Student Emergency Contact field with a couple of validation rules, an error message will display if the regular expression (standard NZ phone number) format is not met.
        [Required, RegularExpression(@"^\+?\d{1,3}[- ]?\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$", ErrorMessage = "Invalid phone number format (please include +64)")]
        public string Emergency_Contact { get; set; }

        //List representing a one to many relationship - one student can take many courses.
        public List<Course> Courses { get; set; }
    }
}
