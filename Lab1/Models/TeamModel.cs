using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lagnamn är obligatoriskt.")]
        public string TeamName { get; set; }

        public string Coach { get; set; }
    }
}