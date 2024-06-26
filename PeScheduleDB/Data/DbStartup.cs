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

                //Statement to ensure the database is created when the project is run.
                Context.Database.EnsureCreated();

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
                new Student { LastName = "Doe", FirstName = "Tim", Email = "TimDoe@gmail.com", YearLevel = 13, Emergency_Contact = "+640221876543"},
                new Student { LastName = "Doe", FirstName = "Mary", Email = "MaryDoe@gmail.com", YearLevel = 9, Emergency_Contact = "+640221543789"},
                new Student { LastName = "Doe", FirstName = "Henrik", Email = "HenrikDoe@gmail.com",
                  YearLevel = 11, Emergency_Contact = "+640221657843"},
                new Student { LastName = "Doe", FirstName = "Carol", Email = "CarolDoe@gmail.com", YearLevel = 13, Emergency_Contact = "+640221345678"},
                new Student { LastName = "Doe", FirstName = "Patrick", Email = "PatrickDoe@gmail.com", YearLevel = 12, Emergency_Contact = "+640221876543"},
                new Student { LastName = "Doe", FirstName = "Monty", Email = "MontyDoe@gmail.com", YearLevel = 10, Emergency_Contact = "+640221987654"},
                new Student { LastName = "Doe", FirstName = "Hellen", Email = "HellenDoe@gmail.com",
                  YearLevel = 10, Emergency_Contact = "+640221768432"},
                new Student { LastName = "Doe", FirstName = "Vijay", Email = "VijayDoe@gmail.com", YearLevel = 13, Emergency_Contact = "+640223874569"},
                };
                Context.Student.AddRange(Students);
                Context.SaveChanges();

                var Teachers = new Teacher[]
                {
                new Teacher { LastName = "Davies", FirstName = "Sarah", Email = "dav@avcol.school.nz", TeacherCode = "DAV" },
                new Teacher { LastName = "Buchanan", FirstName = "Joseph", Email = "bhn@avcol.school.nz", TeacherCode = "BHN" },
                new Teacher { LastName = "Chamley", FirstName = "Lydia", Email = "chy@avcol.school.nz", TeacherCode = "CHY" },
                new Teacher { LastName = "Dewsnap", FirstName = "Dave", Email = "dsp@avcol.school.nz", TeacherCode = "DSP" },
                new Teacher { LastName = "Eccles", FirstName = "Gavin", Email = "els@avcol.school.nz", TeacherCode = "ELS" },
                new Teacher { LastName = "Goldthorpe", FirstName = "Meg", Email = "gdt@avcol.school.nz", TeacherCode = "GDT" },
                new Teacher { LastName = "Hita", FirstName = "Peter", Email = "hta@avcol.school.nz", TeacherCode = "HTA" },
                new Teacher { LastName = "Johnston", FirstName = "Ross", Email = "jhr@avcol.school.nz", TeacherCode = "JHR" },
                new Teacher { LastName = "Joynes", FirstName = "Emma", Email = "jyn@avcol.school.nz", TeacherCode = "JYN" },
                new Teacher { LastName = "Nahi", FirstName = "Claire", Email = "nhi@avcol.school.nz", TeacherCode = "NHI" },
                new Teacher { LastName = "Page", FirstName = "Liz", Email = "pal@avcol.school.nz", TeacherCode = "PAL" },
                new Teacher { LastName = "Piper-Healion", FirstName = "Jay", Email = "phn@avcol.school.nz", TeacherCode = "PHN" },
                new Teacher { LastName = "Sykes", FirstName = "Neil", Email = "sks@avcol.school.nz", TeacherCode = "SKS" },
                new Teacher { LastName = "Went", FirstName = "James", Email = "wnt@avcol.school.nz", TeacherCode = "WNT" }
                };
                Context.Teacher.AddRange(Teachers);
                Context.SaveChanges();

                var Courses = new Course[]
                {
                new Course { CourseName = "9HPE", TeacherId = 1 },
                new Course { CourseName = "10HPE", TeacherId = 2 },
                new Course { CourseName = "10SPC", TeacherId = 3 },
                new Course { CourseName = "11HPE", TeacherId = 4 },
                new Course { CourseName = "11SPC", TeacherId = 5 },
                new Course { CourseName = "11HLT", TeacherId = 6 },
                new Course { CourseName = "12HPE", TeacherId = 7 },
                new Course { CourseName = "12SPC", TeacherId = 8 },
                new Course { CourseName = "12HLT", TeacherId = 9 },
                new Course { CourseName = "13HPE", TeacherId = 10 },
                new Course { CourseName = "13SPC", TeacherId = 11 },
                new Course { CourseName = "13CPE", TeacherId = 12 },
                new Course { CourseName = "13PED", TeacherId = 13 },
                new Course { CourseName = "13OED", TeacherId = 14 },
                new Course { CourseName = "13SPL", TeacherId = 1 }
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
                new Schedule { CourseId = 1, LocationId = 1, Date = new DateTime(2024, 7, 22, 9, 15, 0)},
                new Schedule { CourseId = 2, LocationId = 2, Date = new DateTime(2024, 7, 22, 10, 0, 0)},
                new Schedule { CourseId = 3, LocationId = 3, Date = new DateTime(2024, 7, 23, 11, 30, 0)},
                new Schedule { CourseId = 4, LocationId = 4, Date = new DateTime(2024, 7, 23, 12, 45, 0)},
                new Schedule { CourseId = 5, LocationId = 1, Date = new DateTime(2024, 7, 24, 14, 0, 0)},
                new Schedule { CourseId = 6, LocationId = 2, Date = new DateTime(2024, 7, 24, 9, 30, 0)},
                new Schedule { CourseId = 7, LocationId = 3, Date = new DateTime(2024, 7, 25, 10, 45, 0)},
                new Schedule { CourseId = 8, LocationId = 4, Date = new DateTime(2024, 7, 25, 11, 0, 0)},
                new Schedule { CourseId = 9, LocationId = 1, Date = new DateTime(2024, 7, 26, 12, 15, 0)},
                new Schedule { CourseId = 10, LocationId = 2, Date = new DateTime(2024, 7, 26, 13, 30, 0)},
                new Schedule { CourseId = 11, LocationId = 3, Date = new DateTime(2024, 7, 27, 14, 45, 0)},
                new Schedule { CourseId = 12, LocationId = 4, Date = new DateTime(2024, 7, 27, 9, 0, 0)},
                new Schedule { CourseId = 13, LocationId = 1, Date = new DateTime(2024, 7, 28, 10, 15, 0)},
                new Schedule { CourseId = 14, LocationId = 2, Date = new DateTime(2024, 7, 28, 11, 30, 0)}
                };
                Context.Schedule.AddRange(Schedules);
                Context.SaveChanges();
            }
        }
    }

}
