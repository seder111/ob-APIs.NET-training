using APIVersionControl.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace APIVersionControl.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private const string ApiTestURL = "https://dummyapi.io/data/v1/user?limit=30";
        private const string AppID = "631626c880ee9957fbe558b2";
        private readonly HttpClient _http;

        public UsersController(HttpClient http)
        {
            _http = http;
        }

        [MapToApiVersion("2.0")]
        [HttpGet(Name = "GetUsersData")]
        public async Task<IActionResult> GetUsersDataAsync()
        {
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("app-id", AppID);

            var response = await _http.GetStreamAsync(ApiTestURL);
            var usersData = await JsonSerializer.DeserializeAsync<UsersResponseData>(response);

            var users = usersData?.data;

            return Ok(users);
        }

    }
}
