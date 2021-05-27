using KitchenAid.Model.Inventory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    public class Storages
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/storages");

        public async Task<Storage[]> GetStoragesAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Storage[]>(json);

        }

        public async Task<Product[]> GetProductsAsync(int storageId)
        {
            HttpResponseMessage result = await _httpClient.GetAsync(new Uri(baseUri, $"storages/{storageId}/products"));
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product[]>(json);
        }

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

        internal async Task<bool> DeleteStorageAsync(Storage storage)
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(new Uri(baseUri, $"storages/{storage.StorageId}"));
            return result.IsSuccessStatusCode;
        }


        internal async Task<Storage> GetInventoryAsync()
        {
            int id = 1;
            HttpResponseMessage result = await _httpClient.GetAsync(new Uri(baseUri, $"storages/{id}"));
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Storage>(json);
        }
    }
}
