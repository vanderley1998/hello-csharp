using Microsoft.AspNetCore.Http;
using Plans.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Plans.McvApplication.HttpClients
{
    public class PlanApiClient
    {

        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _accessor;

        public PlanApiClient(HttpClient httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _accessor = accessor;
        }

        private void AddBearerToken()
        {
            var token = _accessor.HttpContext.User.Claims.First(c => c.Type == "Token").Value;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<PlanApi> GetPlanAsync(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(""); // preencher aqui
            HttpResponseMessage response = await httpClient.GetAsync($"Plan/{id}");
            response.EnsureSuccessStatusCode();
            var model = await response.Content.ReadA.sAsync<PlanApi>();
        }

    }
}