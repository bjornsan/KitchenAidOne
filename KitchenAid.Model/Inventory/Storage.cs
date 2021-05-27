using KitchenAid.Model.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KitchenAid.Model.Inventory
{
    public enum KindOfStorage
    {
        MainInventory,
        Fridge,
        Freezer,
        DryStorage,
        ShoppingList
    }

    public class Storage
    {
        public int StorageId { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public KindOfStorage KindOfStorage { get; set; }


        public ICollection<StorageProduct> Products { get; set; } = new List<StorageProduct>();
    }
}
