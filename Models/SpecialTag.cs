using System.ComponentModel.DataAnnotations;

namespace GraniteHouse.Models
{
    public class SpecialTag
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}