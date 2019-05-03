using Plans.Api.Models;
using Plans.Api.Models.Extensions;
using Plans.Models.Plans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Plans.McvApplication.Requests
{
    public class PlanApiClient
    {

        private readonly HttpClient _httpClient;

        public PlanApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:6000/api/v1.0/");
        }

        public async Task<List<PlanApi>> GetPlans()
        {
            string token = await new AuthApiClient().GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync("Plan");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<List<PlanApi>>();
        }

        public async Task<PlanApi> GetPlan(int id)
        {
            string token = await new AuthApiClient().GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.GetAsync($"Plan/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PlanApi>();
        }

        public async Task<PlanApi> Save(Plan plan)
        {
            string token = await new AuthApiClient().GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            PlanApi planToApi = plan.ToPlanApi();
            HttpResponseMessage response;
            if (planToApi.Id == 0)
            {
                response = await _httpClient.PostAsJsonAsync<PlanApi>($"Plan", planToApi);
            } else
            {
                response = await _httpClient.PutAsJsonAsync<PlanApi>($"Plan", planToApi);
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PlanApi>();
        }

        public async Task<PlanApi> Delete(int id)
        {
            string token = await new AuthApiClient().GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Plan/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<PlanApi>();
        }
    }
}