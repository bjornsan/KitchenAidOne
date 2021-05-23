using System;

namespace KitchenAid.Model.Inventory
{
    public class PriceHistory
    {
        public int PriceHistoryId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public string PriceUnit { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
