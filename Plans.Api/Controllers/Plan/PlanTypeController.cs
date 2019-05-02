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
    public class PlanTypeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] PlanType planType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    planType.Id = 0;
                    var createdPlanType = ConnectionDB.PlansModule.DataPlanType.Save(planType);
                    if (createdPlanType != null) { return Ok(new { createdPlanType.Id }); }
                }
                return BadRequest("The plan'type object received isn't valid");
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
                var planType = ConnectionDB.PlansModule.DataPlanType.Get(id);
                return Ok(planType);
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
            var list = ConnectionDB.PlansModule.DataPlanType.GetAll().ToList();
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var planTypeFlag = ConnectionDB.PlansModule.DataPlanType.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanType planType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (planType.Id <= 0) { return BadRequest($"The plan'type id is required or is invalid: {planType.Id}"); }
                    var updatedPlanType = ConnectionDB.PlansModule.DataPlanType.Save(planType);
                    if (updatedPlanType != null) { return Ok(); }
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