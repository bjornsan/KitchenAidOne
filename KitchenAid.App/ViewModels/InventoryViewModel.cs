using KitchenAid.App.DataAccess;
using KitchenAid.App.Helpers;
using KitchenAid.Model.Inventory;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitchenAid.App.ViewModels
{
    /// <summary>View Model for the InventoryPage</summary>
    public class InventoryViewModel : Observable
    {
        /// <summary>Gets or sets the delete storage command.</summary>
        /// <value>The delete storage command.</value>
        public ICommand DeleteStorageCommand { get; set; }
        /// <summary>Gets or sets the update product command.</summary>
        /// <value>The update product command.</value>
        public ICommand UpdateProductCommand { get; set; }
        /// <summary>Gets or sets the delete product command.</summary>
        /// <value>The delete product command.</value>
        public ICommand DeleteProductCommand { get; set; }
        /// <summary>Gets the storages.</summary>
        /// <value>The storages.</value>
        public ObservableCollection<Storage> Storages { get; } = new ObservableCollection<Storage>();
        /// <summary>Gets the products.
        /// For a spesific storage.</summary>
        /// <value>The products.</value>
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

        /// <summary>The storage data access</summary>
        public Storages storageDataAccess = new Storages();
        /// <summary>The product data access</summary>
        public Products productDataAccess = new Products();
        /// <summary>The storage product data access</summary>
        public StorageProducts storageProductDataAccess = new StorageProducts();

        /// <summary>The selected storage, from UI</summary>
        private Storage selectedStorage;

        /// <summary>Gets or sets the selected storage.</summary>
        /// <value>The selected storage.</value>
        public Storage SelectedStorage
        {
            get => selectedStorage;
            set
            {
                Set(ref selectedStorage, value);

                if (value != null)
                    LoadProductsForStorageAsync(((Storage)value).StorageId);
            }
        }

        /// <summary>The selected product
        /// in a selected storage</summary>
        private Product selectedProduct;

        /// <summary>Gets or sets the selected product.</summary>
        /// <value>The selected product.</value>
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                Set(ref selectedProduct, value);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="InventoryViewModel" /> class.</summary>
        public InventoryViewModel()
        {
            UpdateProductCommand = new RelayCommand<Product>(async product =>
            {
                if (await productDataAccess.UpdateProductAsync(product))
                {
                    Products.Clear();
                    LoadProductsForStorageAsync(Storages.First().StorageId);
                }
            });

            DeleteProductCommand = new RelayCommand<Product>(async param =>
            {
                if (await productDataAccess.DeleteProductAsync(param as Product))
                    Products.Remove(param);
            }, param => param != null);

            DeleteStorageCommand = new RelayCommand<Storage>(async storage =>
            {
                if (await storageDataAccess.DeleteStorageAsync(storage))
                    Storages.Remove(storage);
            }, param => param != null);
        }


        /// <summary>Loads the products for a spesific storage asynchronous.</summary>
        /// <param name="storageId">The storage identifier.</param>
        internal async void LoadProductsForStorageAsync(int storageId)
        {
            var products = await storageDataAccess.GetProductsAsync(storageId);
            Products.Clear();

            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }

        /// <summary>Loads the storages asynchronous.</summary>
        internal async Task LoadDataAsync()
        {
            var storages = await storageDataAccess.GetStoragesAsync();

            foreach (Storage storage in storages)
                Storages.Add(storage);
        }
    }
}
