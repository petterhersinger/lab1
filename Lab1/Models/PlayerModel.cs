using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn är obligatoriskt.")]
        public string Name { get; set; }
        public bool IsStarter { get; set; }
        public string Position { get; set; }

    }
}
