using System.ComponentModel.DataAnnotations;

namespace T1TeenFit.Core.Models
{
    public class Activity
    {
        // Activity model represents the activity table in the database 

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Tips { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}