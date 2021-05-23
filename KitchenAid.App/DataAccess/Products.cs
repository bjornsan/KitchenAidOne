using KitchenAid.Model.Inventory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    public class Products
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/products");


        public async Task<Product[]> GetProductsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product[]>(json);
        }


        internal async Task<bool> AddProductAsync(Product product)
        {
            string json = JsonConvert.SerializeObject(product);
            HttpResponseMessage result = await _httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedProduct = JsonConvert.DeserializeObject<Product>(json);
                product.ProductId = returnedProduct.ProductId;

                return true;
            }
            else
                return false;
        }

        internal async Task<bool> UpdateProductAsync(Product product)
        {
            if (product != null)
            {
                string json = JsonConvert.SerializeObject(product);
                HttpResponseMessage result = await _httpClient.PutAsync($"{baseUri}/{product.ProductId}", new StringContent(json, Encoding.UTF8, "application/json"));

                if (result.IsSuccessStatusCode)
                {
                    //(json = await result.Content.ReadAsStringAsync();
                    // var returnedStorage = JsonConvert.DeserializeObject<Storage>(json);
                    // storage.StorageId = returnedStorage.StorageId;
                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        internal async Task<bool> DeleteProductAsync(Product product)
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(new Uri(baseUri, $"categories/{product.ProductId}"));
            return result.IsSuccessStatusCode;
        }
    }
}
