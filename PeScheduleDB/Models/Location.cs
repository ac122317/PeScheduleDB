using System.ComponentModel.DataAnnotations;
namespace PeScheduleDB.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required, MaxLength(25)]
        public string LocationName { get; set; }
    }
}
