using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Inventory
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }


        public ICollection<Product> Products = new List<Product>();

        public override string ToString() => $"{Name}";
    }
}
