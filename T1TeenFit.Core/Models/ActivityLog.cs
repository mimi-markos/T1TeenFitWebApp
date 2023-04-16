using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T1TeenFit.Core.Models
{
    public class ActivityLog
    {
        // Activity log model represents the activity log table in the database

        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ActivityDate { get; set; }


        [Required] //("minutes" - to be displayed on user interface input form)
        public int Duration { get; set; }


        [Required] //("mmol/L" (standard measurement of glucose) - to be displayed on user interface form)
        public double GlucoseBeforeActivity { get; set; }

        [Required] //("mmol/L" (standard measurement of glucose) - to be displayed on user interface form)
        public double GlucoseAfterActivity { get; set; }


        [StringLength(500, MinimumLength = 10)]
        public string ActivityNotes { get; set; }


        [ForeignKey("Activity")]
        public int ActivityId { get; set; }


        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }


        //navigation property
        public Activity Activity { get; set; }
    }
}
