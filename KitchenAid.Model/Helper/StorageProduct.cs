using KitchenAid.Model.Inventory;

namespace KitchenAid.Model.Helper
{
    /// <summary>Used by entity framework to model the relationship<br />between storage and product.</summary>
    public class StorageProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
