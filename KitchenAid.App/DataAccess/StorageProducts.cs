using KitchenAid.Model.Helper;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    public class StorageProducts
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/storageproducts");

        internal async Task<bool> AddStorageProductAsync(StorageProduct storageProduct)
        {
            string json = JsonConvert.SerializeObject(storageProduct);
            HttpResponseMessage result = await _httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedStorageProduct = JsonConvert.DeserializeObject<StorageProduct>(json);
                storageProduct.StorageId = returnedStorageProduct.StorageId;

                return true;
            }
            else
                return false;
        }
    }
}
