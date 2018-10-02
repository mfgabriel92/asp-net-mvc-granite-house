using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraniteHouse.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        [Required]
        [Display(Name="Available")]
        public bool IsAvailable { get; set; } = false;

        public string Image { get; set; }

        public string ShadeColor { get; set; }

        public int ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        [Display(Name="Product Type")]
        public virtual ProductType ProductType { get; set; }

        public int SpecialTagId { get; set; }

        [ForeignKey("SpecialTagId")]
        [Display(Name="Special Tag")]
        public virtual SpecialTag SpecialTag { get; set; }
    }
}