using KitchenAid.App.Helpers;
using KitchenAid.Model.Inventory;

namespace KitchenAid.App.ViewModels
{
    public class ProductViewModel : Observable
    {
        public ProductViewModel(Product model = null) => Model = model ?? new Product();

        private Product _model;

        /// <summary>
        /// Gets or sets the underlying Customer object.
        /// </summary>
        public Product Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    // RefreshProducts();

                    // Raise the PropertyChanged event for all properties.
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Name
        {
            get => Model.Name;
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    IsModified = true;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's last name.
        /// </summary>
        public double Quantity
        {
            get => Model.Quantity;
            set
            {
                if (value != Model.Quantity)
                {
                    Model.Quantity = value;
                    IsModified = true;
                    OnPropertyChanged(nameof(Quantity));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's last name.
        /// </summary>
        public string QuantityUnit
        {
            get => Model.QuantityUnit;
            set
            {
                if (value != Model.QuantityUnit)
                {
                    Model.QuantityUnit = value;
                    IsModified = true;
                    OnPropertyChanged(nameof(QuantityUnit));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's last name.
        /// </summary>
        public double CurrentPrice
        {
            get => Model.CurrentPrice;
            set
            {
                if (value != Model.CurrentPrice)
                {
                    Model.CurrentPrice = value;
                    IsModified = true;
                    OnPropertyChanged(nameof(CurrentPrice));
                }
            }
        }
    }
}









