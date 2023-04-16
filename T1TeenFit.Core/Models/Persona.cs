using System.ComponentModel.DataAnnotations;
using T1TeenFit.Core.Enums;

namespace T1TeenFit.Core.Models
{
    public class Persona
    {
        // Persona model represents the persona table in the database

        [Key]
        public int Id { get; set; }

        [Required]
        public PersonaType PersonaType { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}