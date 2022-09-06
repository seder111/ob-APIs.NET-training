using ApiStoreOB.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiStoreOB.Controllers.V2
{
    [ApiVersion("2.0")]
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

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetProductData")]
        public async Task<IActionResult> GetProductDataAsunc()
        {
            _httpClient.DefaultRequestHeaders.Clear();

            var response = await _httpClient.GetStreamAsync(ApiTestURL);

            var product = await JsonSerializer.DeserializeAsync<ProductGuid[]>(response);

            return Ok(product);
        }

    }
}
