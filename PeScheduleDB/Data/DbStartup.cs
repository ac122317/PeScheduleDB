using PeScheduleDB.Models;

namespace PeScheduleDB.Data
{
    public class DataforDb
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var Context = serviceScope.ServiceProvider.GetService<PeScheduleDBContext>();

                var Students = new Student[]
                {
                new Student { LastName = "Doe", FirstName = "John", Email = "JohnDoe@gmail.com", Emergency_Contact = "Mum"},
                new Student { LastName = "Doe", FirstName = "Jane", Email = "JaneDoe@gmail.com", Emergency_Contact = "Dad"},
                new Student { LastName = "Doe", FirstName = "Henry", Email = "HenryDoe@gmail.com", Emergency_Contact = "Mum"},
                new Student { LastName = "Doe", FirstName = "Klaus", Email = "KlausDoe@gmail.com", Emergency_Contact = "Dad"},
                };
                Context.Student.AddRange(Students);
                Context.SaveChanges();

                var Courses = new Course[]
                {
                new Course { CourseName = "10HPE", TeacherId = 1},
                new Course { CourseName = "11SPL", TeacherId = 2},
                new Course { CourseName = "11SPL", TeacherId = 2},
                new Course { CourseName = "11OED", TeacherId = 1},
                };
                Context.Course.AddRange(Courses);
                Context.SaveChanges();

                var Teachers = new Teacher[]
                {
                new Teacher { LastName = "Bucannan", FirstName = "Mr", Email = "BHN@avcol.school.nz", TeacherCode = "BHN"},
                new Teacher { LastName = "Bla", FirstName = "Mr", Email = "BLD@avcol.school.nz", TeacherCode = "BLD"},
                };
                Context.Teacher.AddRange(Teachers);
                Context.SaveChanges();
            }
        }
    }

}
