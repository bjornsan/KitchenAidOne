using KitchenAid.Model.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Inventory
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public double Quantity { get; set; }

        public string QuantityUnit { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be set to more than 1")]
        public double CurrentPrice { get; set; }

        public KindOfStorage StoredIn { get; set; }



        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<StorageProduct> Storages { get; } = new List<StorageProduct>();
    }
}
