using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Plans.McvApplication.Requests
{
    public class TypeApiClient
    {
        private readonly HttpClient _httpClient;

        public TypeApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:6000/api/v1.0/");
        }

        public async Task<List<PlanType>> GetPlans()
        {
            string token = await new AuthApiClient().GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync("PlanType");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<List<PlanType>>();
        }
    }
}