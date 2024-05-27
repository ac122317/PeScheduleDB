using Microsoft.AspNetCore.Identity;

namespace PeScheduleDB.ScheduleUser
{
    public class ScheduleUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
