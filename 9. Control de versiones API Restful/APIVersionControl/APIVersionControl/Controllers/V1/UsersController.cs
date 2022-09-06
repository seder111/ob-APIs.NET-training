using APIVersionControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APIVersionControl.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";

        private const string AppID = "631626c880ee9957fbe558b2";

        private readonly HttpClient _http;

        public UsersController(HttpClient httpClient)
        {
            _http = httpClient;
        }

        [MapToApiVersion("1.0")]
        [HttpGet(Name ="GetUserData")]
        public async Task<IActionResult> GetUserDataAsync()
        {
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("app-id", AppID);

            var response = await _http.GetStreamAsync(ApiTestURL);
            var userData = await JsonSerializer.DeserializeAsync <UsersResponseData>(response);
            return Ok(userData);

        }
    }
}
