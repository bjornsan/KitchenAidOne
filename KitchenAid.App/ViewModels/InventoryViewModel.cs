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
    public class InventoryViewModel : Observable
    {
        public ICommand DeleteStorageCommand { get; set; }
        public ICommand UpdateProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ObservableCollection<Storage> Storages { get; } = new ObservableCollection<Storage>();
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

        public Storages storageDataAccess = new Storages();
        public Products productDataAccess = new Products();
        public StorageProducts storageProductDataAccess = new StorageProducts();

        private Storage selectedStorage;

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

        private Product selectedProduct;

        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                Set(ref selectedProduct, value);
            }
        }

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
                //var storageProduct = await productDataAccess.GetStorageProductForProductsAsync(((Product)param).ProductId);
                //if (await storageProductDataAccess.DeleteStorageProductAsync(storageProduct))

                if (await productDataAccess.DeleteProductAsync(param as Product))
                    Products.Remove(param);
            }, param => param != null);

            DeleteStorageCommand = new RelayCommand<Storage>(async storage =>
            {
                if (await storageDataAccess.DeleteStorageAsync(storage))
                    Storages.Remove(storage);
            }, param => param != null);
        }


        internal async void LoadProductsForStorageAsync(int storageId)
        {
            var products = await storageDataAccess.GetProductsAsync(storageId);
            Products.Clear();

            foreach (Product product in products)
            {
                Products.Add(product);
            }
        }

        public async Task LoadDataAsync()
        {
            var storages = await storageDataAccess.GetStoragesAsync();

            foreach (Storage storage in storages)
                Storages.Add(storage);
        }
    }
}
