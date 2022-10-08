using System.ComponentModel.DataAnnotations;

namespace PlaterformService.Dtos
{
    public class PlateformReadDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
     
        public string? Publisher { get; set; }
     
        public string? Cost { get; set; }
    }
}
