using KitchenAid.Model.Inventory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KitchenAid.App.DataAccess
{
    /// <summary>Data access for products</summary>
    public class Products
    {
        /// <summary>The HTTP client</summary>
        readonly HttpClient _httpClient = new HttpClient();
        /// <summary>The base URI</summary>
        static readonly Uri baseUri = new Uri("http://localhost:36878/api/products");


        /// <summary>Gets the products asynchronous.</summary>
        /// <returns>
        ///   An array of Type Product.
        /// </returns>
        public async Task<Product[]> GetProductsAsync()
        {
            HttpResponseMessage result = await _httpClient.GetAsync(baseUri);
            string json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Product[]>(json);
        }

        /// <summary>Adds the product asynchronous.</summary>
        /// <param name="product">The product.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
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

        /// <summary>Updates the product asynchronous.</summary>
        /// <param name="product">The product.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
        internal async Task<bool> UpdateProductAsync(Product product)
        {
            if (product != null)
            {
                string json = JsonConvert.SerializeObject(product);
                HttpResponseMessage result = await _httpClient.PutAsync($"{baseUri}/{product.ProductId}", new StringContent(json, Encoding.UTF8, "application/json"));

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }


        /// <summary>Deletes the product asynchronous.</summary>
        /// <param name="product">The product.</param>
        /// <returns>
        ///   A bool if it was successfull or not.
        /// </returns>
        internal async Task<bool> DeleteProductAsync(Product product)
        {
            HttpResponseMessage result = await _httpClient.DeleteAsync(new Uri(baseUri, $"products/{product.ProductId}"));
            return result.IsSuccessStatusCode;
        }
    }
}
