using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Api.Models;
using Plans.Api.Models.Extensions;

namespace Plans.Api.Controllers.Plan
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlanHistoryController : ControllerBase, IServicesApi<PlanHistoryApi>
    {
        public ISet<int> CacheIds { get; }

        public PlanHistoryController()
        {
            CacheIds = ConnectionDB.PlansModule.DataPlansHistory.GetAll().Select(ph => ph.Id).ToHashSet();
        }

        [HttpGet("{id}")]
        public IActionResult ListById(int id)
        {
            try
            {
                var idPlanHistory = CacheIds.First(ph => ph == id);
                var list = ConnectionDB.PlansModule.DataPlansHistory.GetById(idPlanHistory).Select(ph => ph.ToPlanHistoryApi()).ToList();
                return Ok(list);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan with id = {id}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanHistoryApi objApi)
        {
            return NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            return NotFound();
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanHistoryApi objApi)
        {
            return NotFound();
        }

        public IActionResult Get(int id)
        {
            return NotFound();
        }
    }
}