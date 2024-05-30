using Microsoft.AspNetCore.Identity;

namespace PeScheduleDB.Areas.Identity.Data;

public class ScheduleUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
