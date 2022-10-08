using System.ComponentModel.DataAnnotations;

namespace PlatefromService.Models
{
    public class Plateform
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Publisher { get; set; }

        [Required]
        public string? Cost { get; set; }
    }
}