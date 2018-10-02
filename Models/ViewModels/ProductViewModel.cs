using System.Collections.Generic;

namespace GraniteHouse.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<ProductType> ProductType { get; set; }
        public IEnumerable<SpecialTag> SpecialTag { get; set; }
    }
}