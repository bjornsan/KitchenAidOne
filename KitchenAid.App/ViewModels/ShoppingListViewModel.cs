
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
    /// <summary>View Model for the shopping list page</summary>
    public class ShoppingListViewModel : Observable
    {

        /// <summary>Gets or sets the add product command.</summary>
        /// <value>The add product command.</value>
        public ICommand AddProductCommand { get; set; }

        /// <summary>Gets the shoppinglist</summary>
        /// <value>The shopping list.</value>
        public ObservableCollection<Storage> ShoppingLists { get; } = new ObservableCollection<Storage>();
        /// <summary>Gets the products in the shoppinglist.</summary>
        /// <value>The products.</value>
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        /// <summary>Gets all products stored in database.</summary>
        /// <value>All products.</value>
        public ObservableCollection<Product> AllProducts { get; } = new ObservableCollection<Product>();
        /// <summary>Gets the categories.</summary>
        /// <value>The categories.</value>
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        /// <summary>The kind of storages</summary>
        public List<KindOfStorage> KindOfStorages = Enum.GetValues(typeof(KindOfStorage)).Cast<KindOfStorage>().ToList();


        /// <summary>The product data access</summary>
        private readonly Products productDataAccess = new Products();
        /// <summary>The storage data access</summary>
        private readonly Storages storageDataAccess = new Storages();
        /// <summary>The category data access</summary>
        private readonly Categories categoryDataAccess = new Categories();
        /// <summary>The storage product data access</summary>
        private readonly StorageProducts storageProductDataAccess = new StorageProducts();


        /// <summary>The selected shopping list</summary>
        private Storage selectedShoppingList;

        /// <summary>Gets or sets the selected shopping list.</summary>
        /// <value>The selected shopping list.</value>
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

        /// <summary>Initializes a new instance of the <see cref="ShoppingListViewModel" /> class.</summary>
        public ShoppingListViewModel()
        {
            AddProductCommand = new RelayCommand<List<Product>>(async listOfProducts =>
            {
                foreach (Product product in listOfProducts)
                    await productDataAccess.AddProductAsync(product);
            });

        }

        /// <summary>Loads the products for storage asynchronous.</summary>
        /// <param name="storageId">The storage identifier.</param>
        internal async void LoadProductsForStorageAsync(int storageId)
        {
            var products = await storageDataAccess.GetProductsAsync(storageId);

            foreach (Product product in products)
                Products.Add(product);
        }

        /// <summary>Gets the shopping list.</summary>
        /// <returns>
        ///   The shoppinglist
        /// </returns>
        public Storage GetShoppingList() => ShoppingLists[0];

        /// <summary>Creates the shopping list.</summary>
        public void CreateShoppingList()
        {
            var shoppingList = new Storage { CreatedOn = DateTime.Now, KindOfStorage = KindOfStorage.ShoppingList };
            ShoppingLists.Add(shoppingList);

            AddStorageAsync(shoppingList);
        }

        /// <summary>Adds the shoppinglist asynchronous.</summary>
        /// <param name="shoppinglist">The shoppinglist.</param>
        private async void AddStorageAsync(Storage shoppinglist)
        {
            await storageDataAccess.AddStorageAsync(shoppinglist);
        }

        /// <summary>Adds the storage product asynchronous.</summary>
        /// <param name="storageProduct">The storage product.</param>
        public async void AddStorageProductAsync(StorageProduct storageProduct)
        {
            await storageProductDataAccess.AddStorageProductAsync(storageProduct);
        }

        /// <summary>Gets the categories asynchronous.</summary>
        internal async void GetCategoriesAsync()
        {
            var catagories = await categoryDataAccess.GetCategoriesAsync();

            foreach (Category category in catagories)
                Categories.Add(category);
        }

        /// <summary>
        /// Updates the inventory asynchronous.
        /// Getting all the products from the database, and checking if the shoppinglist and the inventory has
        /// the product already, if so the product should be updated.Otherwise the product is added to shoppinglist
        /// and inventory.
        /// </summary>
        /// <param name="shoppinglist">The shoppinglist.</param>
        /// <param name="product">The product.</param>
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

                    await productDataAccess.UpdateProductAsync(product);
                }
            }
        }

        /// <summary>Gets all products in database asynchronous.</summary>
        internal async void GetAllProductsAsync()
        {
            var allproducts = await productDataAccess.GetProductsAsync();

            foreach (Product product in allproducts)
                AllProducts.Add(product);
        }

        /// <summary>Checks if a product exists in a list</summary>
        /// <param name="productsInList">The products in list.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   bool if it exists or not.
        /// </returns>
        private bool ProductInList(ICollection<Product> productsInList, int id)
        {
            foreach (var product in productsInList)
            {
                if (product.ProductId == id)
                    return true;
            }
            return false;
        }

        /// <summary>Override of ProductInList, running on an array.</summary>
        /// <param name="productsInList">The products in list.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   bool if it exists or not.
        /// </returns>
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
