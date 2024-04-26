using PeScheduleDB.Models;

namespace PeScheduleDB.DummyData
{
    public class DataForDb
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var Context = serviceScope.ServiceProvider.GetService<PeScheduleDBContext>();

                //The if statement ensures that if there is any existing data amongst any of the models, the method will return to prevent the data being added again (thus being duplicated).

                if (Context.Student.Any() || Context.Teacher.Any() || Context.Course.Any() || Context.Location.Any() || Context.Schedule.Any()) 
                {
                    return;
                }

                var Students = new Student[]
                {
                new Student { LastName = "Doe", FirstName = "John", Email = "JohnDoe@gmail.com", YearLevel = 12, Emergency_Contact = "+640278321892"},
                new Student { LastName = "Doe", FirstName = "Jane", Email = "JaneDoe@gmail.com", YearLevel = 9, Emergency_Contact = "+640221234567"},
                new Student { LastName = "Doe", FirstName = "Henry", Email = "HenryDoe@gmail.com",
                  YearLevel = 10, Emergency_Contact = "+640212345678"},
                new Student { LastName = "Doe", FirstName = "Klaus", Email = "KlausDoe@gmail.com", YearLevel = 11, Emergency_Contact = "+64098134567"},
                };
                Context.Student.AddRange(Students);
                Context.SaveChanges();

                var Teachers = new Teacher[]
                {
                new Teacher { LastName = "Bucannan", FirstName = "Joseph", Email = "BHN@avcol.school.nz", TeacherCode = "BHN"},
                new Teacher { LastName = "Davies", FirstName = "Sarah", Email = "DAV@avcol.school.nz", TeacherCode = "DAV"},
                new Teacher { LastName = "Sykes", FirstName = "Neil", Email = "SKS@avcol.school.nz", TeacherCode = "SKS"},
                new Teacher { LastName = "Nahi", FirstName = "Claire", Email = "NHI@avcol.school.nz", TeacherCode = "NHI"},
                };
                Context.Teacher.AddRange(Teachers);
                Context.SaveChanges();

                var Courses = new Course[]
                {
                new Course { CourseName = "10HPE", TeacherId = 1},
                new Course { CourseName = "11SPL", TeacherId = 2},
                new Course { CourseName = "12OED", TeacherId = 3},
                new Course { CourseName = "11OED", TeacherId = 4},
                };
                Context.Course.AddRange(Courses);
                Context.SaveChanges();

                var Locations = new Location[]
                {
                new Location { LocationName = "Field"},
                new Location { LocationName = "Turf"},
                new Location { LocationName = "Halberg Gym"},
                new Location { LocationName = "Mills Gym"},
                };
                Context.Location.AddRange(Locations);
                Context.SaveChanges();

                var Schedules = new Schedule[]
                {
                new Schedule { CourseId = 1, LocationId = 1, Date = new DateTime(2024, 4, 12, 9, 15, 0)},
                new Schedule { CourseId = 2, LocationId = 2, Date = new DateTime(2024, 4, 12, 10, 35, 0)},
                new Schedule { CourseId = 3, LocationId = 2, Date = new DateTime(2024, 4, 12, 14, 15, 0)},
                new Schedule { CourseId = 4, LocationId = 4, Date = new DateTime(2024, 4, 13, 14, 15, 0)},
                };
                Context.Schedule.AddRange(Schedules);
                Context.SaveChanges();
            }
        }
    }

}
