using KitchenAid.Model.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Inventory
{
    /// <summary>Models a product</summary>
    public class Product
    {
        /// <summary>Gets or sets the product identifier.</summary>
        /// <value>The product identifier.</value>
        public int ProductId { get; set; }
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [Required]
        public double Quantity { get; set; }

        /// <summary>Gets or sets the quantity unit.</summary>
        /// <value>The quantity unit.</value>
        public string QuantityUnit { get; set; }
        /// <summary>Gets or sets the current price.</summary>
        /// <value>The current price.</value>
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be set to more than 1")]
        public double CurrentPrice { get; set; }

        /// <summary>Gets or sets the stored in.</summary>
        /// <value>The stored in.</value>
        public KindOfStorage StoredIn { get; set; }



        /// <summary>Gets or sets the category identifier.</summary>
        /// <value>The category identifier.</value>
        public int CategoryId { get; set; }
        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public Category Category { get; set; }

        /// <summary>Gets the storages.
        /// Used by entity framework</summary>
        /// <value>The storages.</value>
        public ICollection<StorageProduct> Storages { get; } = new List<StorageProduct>();
    }
}
