using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ApiStoreOB.DTO;

namespace ApiStoreOB.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private const string ApiTestURL = "https://fakestoreapi.com/products?limit=10";

        private readonly HttpClient _httpClient;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [MapToApiVersion("1.0")]
        [HttpGet(Name = "GetProductData")]
        public async Task<IActionResult> GetProductDataAsync()
        {
            // Vaciamos las cabeceras.
            _httpClient.DefaultRequestHeaders.Clear();

            var response = await _httpClient.GetStreamAsync(ApiTestURL);
            var productData = await JsonSerializer.DeserializeAsync<Product[]>(response);

            return Ok(productData);
        }
    }
}
