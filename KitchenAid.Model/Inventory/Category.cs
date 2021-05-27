using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Inventory
{
    /// <summary>Models a productcategory</summary>
    public class Category
    {
        /// <summary>Gets or sets the category identifier.</summary>
        /// <value>The category identifier.</value>
        public int CategoryId { get; set; }
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }


        /// <summary>Used by entity framework</summary>
        public ICollection<Product> Products = new List<Product>();

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString() => $"{Name}";
    }
}
