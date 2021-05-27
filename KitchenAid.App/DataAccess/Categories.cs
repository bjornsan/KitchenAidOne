using KitchenAid.Model.Inventory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    /// <summary>Data access for categories.</summary>
    public class Categories
    {
        /// <summary>The HTTP client</summary>
        private readonly HttpClient _httpClient = new HttpClient();
        /// <summary>The base URI</summary>
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/categories");

        /// <summary>Gets the categories asynchronous.</summary>
        /// <returns>
        ///   An array of Type Category.
        /// </returns>
        public async Task<Category[]> GetCategoriesAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Category[]>(json);

        }
    }
}
