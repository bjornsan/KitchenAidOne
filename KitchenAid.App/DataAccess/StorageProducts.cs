using KitchenAid.Model.Helper;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    /// <summary>Data access for storageproducts.</summary>
    public class StorageProducts
    {
        /// <summary>The HTTP client</summary>
        private readonly HttpClient _httpClient = new HttpClient();
        /// <summary>The base URI</summary>
        private readonly Uri baseUri = new Uri("http://localhost:36878/api/storageproducts");


        /// <summary>Adds the storage product asynchronous.</summary>
        /// <param name="storageProduct">The storage product.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
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
