using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required, MaxLength(25), RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Name must only contain letters or numbers and no spaces/special characters")]
        public string LocationName { get; set; }
    }
}
