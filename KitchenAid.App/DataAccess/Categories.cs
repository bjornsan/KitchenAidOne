using KitchenAid.Model.Inventory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    public class Categories
    {
        readonly HttpClient _httpClient = new HttpClient();
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/categories");

        public async Task<Category[]> GetCategoriesAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Category[]>(json);

        }

        public async Task<Category[]> GetProductsAsync(int categoryId)
        {
            HttpResponseMessage result = await _httpClient.GetAsync(new Uri(baseUri, $"storages/{categoryId}/products"));
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Category[]>(json);
        }

        internal async Task<bool> AddStorageAsync(Category category)
        {
            string json = JsonConvert.SerializeObject(category);
            HttpResponseMessage result = await _httpClient.PostAsync(baseUri, new StringContent(json, Encoding.UTF8, "application/json"));

            if (result.IsSuccessStatusCode)
            {
                json = await result.Content.ReadAsStringAsync();
                var returnedCategory = JsonConvert.DeserializeObject<Category>(json);
                category.CategoryId = returnedCategory.CategoryId;

                return true;
            }
            else
                return false;
        }

        internal async Task<bool> UpdateStorageAsync(Category category)
        {
            if (category != null)
            {
                string json = JsonConvert.SerializeObject(category);
                HttpResponseMessage result = await _httpClient.PutAsync($"{baseUri}/{category.CategoryId}", new StringContent(json, Encoding.UTF8, "application/json"));

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

        internal async Task<bool> DeleteStorageAsync(Category category)
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(new Uri(baseUri, $"categories/{category.CategoryId}"));
            return result.IsSuccessStatusCode;
        }
    }
}
