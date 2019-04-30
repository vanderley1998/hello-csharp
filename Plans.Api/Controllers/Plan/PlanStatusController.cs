using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Models.Plans;

namespace Plans.Api.Controllers.Plan
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlanStatusController : ControllerBase, IServicesApi<PlanStatus>
    {
        public ISet<int> CacheIds { get; }

        public PlanStatusController()
        {
            CacheIds = ConnectionDB.PlansModule.DataPlanStatus.GetAll().Select(ps => ps.Id).ToHashSet();
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanStatus plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    plan.Id = 0;
                    var createdPlanStatus = ConnectionDB.PlansModule.DataPlanStatus.Save(plan);
                    if (createdPlanStatus != null)
                    {
                        CacheIds.Add(createdPlanStatus.Id);
                        return Ok(new { createdPlanStatus.Id });
                    }
                }
                return BadRequest("The plan'status object received isn't valid");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var idPlanStatus = CacheIds.First(ps => ps == id);
                var planStatus = ConnectionDB.PlansModule.DataPlanStatus.Get(idPlanStatus);
                return Ok(planStatus);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan'status with id = {id}");
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            var list = ConnectionDB.PlansModule.DataPlanStatus.GetAll().ToList();
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var idplanStatus = CacheIds.First(ps => ps == id);
                var planStatusFlag = ConnectionDB.PlansModule.DataPlanStatus.Delete(idplanStatus);
                if (planStatusFlag) { CacheIds.Remove(idplanStatus); }
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return NotFound($"There's no plan'status with id = {id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanStatus planStatus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (planStatus.Id <= 0) { return BadRequest($"The plan'status id is required or is invalid: {planStatus.Id}"); }
                    var idPlanStatus = CacheIds.First(ps => ps == planStatus.Id);
                    var updatedPlanStatus = ConnectionDB.PlansModule.DataPlanStatus.Save(planStatus);
                    if (updatedPlanStatus != null)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan'status with id = {planStatus.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}