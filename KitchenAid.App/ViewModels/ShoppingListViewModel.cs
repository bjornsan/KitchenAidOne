
using KitchenAid.App.DataAccess;
using KitchenAid.App.Helpers;
using KitchenAid.Model.Helper;
using KitchenAid.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace KitchenAid.App.ViewModels
{
    public class ShoppingListViewModel : Observable
    {
        // ICOMMANDS
        public ICommand AddProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }

        // Collections of data
        public ObservableCollection<Storage> ShoppingLists { get; } = new ObservableCollection<Storage>();
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public ObservableCollection<Product> AllProducts { get; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        public List<KindOfStorage> KindOfStorages = Enum.GetValues(typeof(KindOfStorage)).Cast<KindOfStorage>().ToList();

        // Data access
        private readonly Products productDataAccess = new Products();
        private readonly Storages storageDataAccess = new Storages();
        private readonly Categories categoryDataAccess = new Categories();
        private readonly StorageProducts storageProductDataAccess = new StorageProducts();


        private Product selectedProduct;

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                Set(ref selectedProduct, value);
            }
        }

        private Storage selectedShoppingList;

        public Storage SelectedShoppingList
        {
            get => selectedShoppingList;
            set
            {
                Set(ref selectedShoppingList, ShoppingLists[0]);

                if (ShoppingLists[0] != null)
                    LoadProductsForStorageAsync(((Storage)ShoppingLists[0]).StorageId);
            }
        }

        public ShoppingListViewModel()
        {
            AddProductCommand = new RelayCommand<List<Product>>(async listOfProducts =>
            {
                foreach (Product product in listOfProducts)
                    await productDataAccess.AddProductAsync(product);
            });

        }

        internal async void LoadProductsForStorageAsync(int storageId)
        {
            var products = await storageDataAccess.GetProductsAsync(storageId);

            foreach (Product product in products)
                Products.Add(product);

        }

        public Storage GetShoppingList() => ShoppingLists[0];

        public void CreateShoppingList()
        {
            var shoppingList = new Storage { CreatedOn = DateTime.Now, KindOfStorage = KindOfStorage.ShoppingList };
            ShoppingLists.Add(shoppingList);

            AddStorageAsync(shoppingList);
        }

        private async void AddStorageAsync(Storage shoppinglist)
        {
            await storageDataAccess.AddStorageAsync(shoppinglist);
        }

        public async void AddStorageProductAsync(StorageProduct storageProduct)
        {
            await storageProductDataAccess.AddStorageProductAsync(storageProduct);
        }

        internal async void GetCategoriesAsync()
        {
            var catagories = await categoryDataAccess.GetCategoriesAsync();

            foreach (Category category in catagories)
                Categories.Add(category);
        }

        public async void UpdateInventoryAsync(Storage shoppinglist = null, Product product = null)
        {
            GetAllProductsAsync();

            if (product != null && shoppinglist != null)
            {
                var inventory = await storageDataAccess.GetInventoryAsync();
                var productsInInventory = await storageDataAccess.GetProductsAsync(inventory.StorageId);
                var shoppinglistProducts = await storageDataAccess.GetProductsAsync(shoppinglist.StorageId);


                var isProductInDatabase = ProductInList(AllProducts, product.ProductId);
                var isProductInShoppingList = ProductInList(shoppinglistProducts, product.ProductId);
                var isProductInInventory = ProductInList(productsInInventory, product.ProductId);


                if (!isProductInDatabase)
                {
                    await productDataAccess.AddProductAsync(product);
                    UpdateInventoryAsync(shoppinglist, product);
                }
                else if (isProductInDatabase && !isProductInShoppingList && !isProductInInventory)
                {
                    var shoppinglistProduct = new StorageProduct() { StorageId = shoppinglist.StorageId, ProductId = product.ProductId };
                    AddStorageProductAsync(shoppinglistProduct);

                    var inventoryProduct = new StorageProduct() { StorageId = inventory.StorageId, ProductId = product.ProductId };
                    AddStorageProductAsync(inventoryProduct);

                    LoadProductsForStorageAsync(shoppinglist.StorageId);


                }
                else if (isProductInDatabase && isProductInShoppingList && !isProductInInventory)
                {
                    var inventoryProduct = new StorageProduct() { StorageId = inventory.StorageId, ProductId = product.ProductId };
                    AddStorageProductAsync(inventoryProduct);
                }
                else if (isProductInDatabase && !isProductInShoppingList && isProductInInventory)
                {
                    var shoppinglistProduct = new StorageProduct() { StorageId = shoppinglist.StorageId, ProductId = product.ProductId };
                    AddStorageProductAsync(shoppinglistProduct);
                }
            }
        }

        internal async void GetAllProductsAsync()
        {
            var allproducts = await productDataAccess.GetProductsAsync();

            foreach (Product product in allproducts)
                AllProducts.Add(product);
        }

        private bool ProductInList(ICollection<Product> productsInList, int id)
        {
            foreach (var product in productsInList)
            {
                if (product.ProductId == id)
                    return true;
            }
            return false;
        }

        private bool ProductInList(Product[] productsInList, int id)
        {
            foreach (var product in productsInList)
            {
                if (product.ProductId == id)
                    return true;
            }
            return false;
        }
    }
}
