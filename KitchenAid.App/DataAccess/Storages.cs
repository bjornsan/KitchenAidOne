using KitchenAid.Model.Inventory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    /// <summary>Data Access for storages.</summary>
    public class Storages
    {
        /// <summary>The HTTP client</summary>
        readonly HttpClient _httpClient = new HttpClient();
        /// <summary>The base URI</summary>
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/storages");

        /// <summary>Gets the storages asynchronous.</summary>
        /// <returns>
        ///   An array of Type Storage.
        /// </returns>
        public async Task<Storage[]> GetStoragesAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Storage[]>(json);

        }

        /// <summary>Gets the products for a given storage asynchronous.</summary>
        /// <param name="storageId">The storage identifier.</param>
        /// <returns>
        ///   An array of Type Product.
        /// </returns>
        public async Task<Product[]> GetProductsAsync(int storageId)
        {
            HttpResponseMessage result = await _httpClient.GetAsync(new Uri(baseUri, $"storages/{storageId}/products"));
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product[]>(json);
        }

        /// <summary>Adds the storage asynchronous.</summary>
        /// <param name="storage">The storage.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
        internal async Task<bool> AddStorageAsync(Storage storage)
        {
            string json = JsonConvert.SerializeObject(storage);
            HttpResponseMessage result = await _httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedStorage = JsonConvert.DeserializeObject<Storage>(json);
                storage.StorageId = returnedStorage.StorageId;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Updates the storage asynchronous.
        /// </summary>
        /// <param name="storage">The storage.</param>
        /// <returns>
        /// A bool if it was successfull or not.
        /// </returns>


        internal async Task<bool> UpdateStorageAsync(Storage storage)
        {
            if (storage != null)
            {
                string json = JsonConvert.SerializeObject(storage);
                HttpResponseMessage result = await _httpClient.PutAsync($"{baseUri}/{storage.StorageId}", new StringContent(json, Encoding.UTF8, "application/json"));

                if (result.IsSuccessStatusCode)
                {
                    json = await result.Content.ReadAsStringAsync();
                    var returnedStorage = JsonConvert.DeserializeObject<Storage>(json);
                    storage.StorageId = returnedStorage.StorageId;
                    return true;
                }
                else
                    return false;
            }

            return false;
        }

        /// <summary>Deletes the storage asynchronous.</summary>
        /// <param name="storage">The storage.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
        internal async Task<bool> DeleteStorageAsync(Storage storage)
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(new Uri(baseUri, $"storages/{storage.StorageId}"));
            return result.IsSuccessStatusCode;
        }


        /// <summary>Gets the main inventory asynchronous.</summary>
        /// <returns>
        ///   Storage
        /// </returns>
        internal async Task<Storage> GetInventoryAsync()
        {
            int id = 1;
            HttpResponseMessage result = await _httpClient.GetAsync(new Uri(baseUri, $"storages/{id}"));
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Storage>(json);
        }
    }
}
