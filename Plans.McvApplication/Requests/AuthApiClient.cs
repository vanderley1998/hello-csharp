using Plans.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Plans.McvApplication.Requests
{
    public class AuthApiClient
    {

        private readonly HttpClient _httpClient;

        public AuthApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
        }

        public async Task<string> GetToken()
        {
            var resp = await _httpClient.PostAsJsonAsync("api/Auth", new { });
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync();
        }

    }
}