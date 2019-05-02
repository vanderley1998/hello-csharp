using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Models.Plans;

namespace Plans.Api.Controllers.Plan
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlanStatusController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] PlanStatus plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    plan.Id = 0;
                    var createdPlanStatus = ConnectionDB.PlansModule.DataPlanStatus.Save(plan);
                    if (createdPlanStatus != null) { return Ok(new { createdPlanStatus.Id }); }
                }
                return BadRequest("The plan'status object received isn't valid");
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var planStatus = ConnectionDB.PlansModule.DataPlanStatus.Get(id);
                return Ok(planStatus);
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var list = ConnectionDB.PlansModule.DataPlanStatus.GetAll().ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var planStatusFlag = ConnectionDB.PlansModule.DataPlanStatus.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
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
                    var updatedPlanStatus = ConnectionDB.PlansModule.DataPlanStatus.Save(planStatus);
                    if (updatedPlanStatus != null) { return Ok(); }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }
    }
}