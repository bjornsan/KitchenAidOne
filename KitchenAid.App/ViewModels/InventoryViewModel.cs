using KitchenAid.App.DataAccess;
using KitchenAid.App.Helpers;
using KitchenAid.Model.Inventory;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KitchenAid.App.ViewModels
{
    public class InventoryViewModel : Observable
    {
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ObservableCollection<Storage> Storages { get; } = new ObservableCollection<Storage>();
        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();

        public Storages storageDataAccess = new Storages();

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

        public InventoryViewModel()
        {
            AddCommand = new RelayCommand<Storage>(async storage =>
            {
                if (storage == null)
                {
                    storage = new Storage() { CreatedOn = DateTime.Now, KindOfStorage = KindOfStorage.Fridge };
                }


                if (await storageDataAccess.AddStorageAsync(storage))
                    Storages.Add(storage);
            });

            UpdateCommand = new RelayCommand<Storage>(async storage =>
            {
                if (await storageDataAccess.UpdateStorageAsync(storage))
                {
                    Storages.Clear();
                    await LoadDataAsync();
                }
            });

            DeleteCommand = new RelayCommand<Storage>(async param =>
            {
                if (await storageDataAccess.DeleteStorageAsync((Storage)param))
                    Storages.Remove(param);
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
