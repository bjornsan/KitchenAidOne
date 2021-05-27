using KitchenAid.Model.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Inventory
{
    /// <summary>
    ///   Enum to help setting the kind of storage that 
    ///   a specific storage is.
    /// </summary>
    public enum KindOfStorage
    {
        MainInventory,
        Fridge,
        Freezer,
        DryStorage,
        ShoppingList
    }

    /// <summary>
    ///   Models a storage.
    /// </summary>
    public class Storage
    {
        /// <summary>Gets or sets the storage identifier.</summary>
        /// <value>The storage identifier.</value>
        public int StorageId { get; set; }
        /// <summary>Gets or sets the created on.</summary>
        /// <value>The created on.</value>
        [Required]
        public DateTime CreatedOn { get; set; }
        /// <summary>Gets or sets the kind of storage.</summary>
        /// <value>The kind of storage.</value>
        [Required]
        public KindOfStorage KindOfStorage { get; set; }


        /// <summary>
        /// Gets or sets the products belonging to the storage.<br />Used by entity frameowrk.
        /// </summary>
        /// <value>The products.</value>
        public ICollection<StorageProduct> Products { get; set; } = new List<StorageProduct>();
    }
}
