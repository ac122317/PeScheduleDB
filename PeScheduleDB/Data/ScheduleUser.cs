using Microsoft.AspNetCore.Identity;

namespace PeScheduleDB.Areas.Identity.Data;

//Custom user class, inherits all the properties from IdentityUser but also has it's own variables, being FirstName and LastName - these are used for custom fields.
public class ScheduleUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
