using KitchenAid.Model.Inventory;

namespace KitchenAid.Model.Helper
{
    public class StorageProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
