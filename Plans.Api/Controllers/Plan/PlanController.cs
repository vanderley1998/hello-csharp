using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Plans.Api.Models;
using Plans.Api.Models.Extensions;
using Plans.Models;
using Plans.Models.Users;

namespace Plans.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        [HttpGet]
        public IActionResult List()
        {
            var list = ConnectionDB.PlansModule.DataPlan.GetAll().Select(p => p.ToPlanApi()).ToList();
            foreach (var plan in list)
            {
                var interestedIdUsers = ConnectionDB.PlansModule.DataPlanInterestedUsers.GetById(plan.Id).Select(p => p.User.Id).ToList();
                plan.InterestedUsers = interestedIdUsers;
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var plan = ConnectionDB.PlansModule.DataPlan.Get(id).ToPlanApi();
                var interestedIdUsers = ConnectionDB.PlansModule.DataPlanInterestedUsers.GetById(id).Select(p => p.User.Id).ToList();
                plan.InterestedUsers = interestedIdUsers;
                return Ok(plan);
            }
            catch (Exception e)
            {
                return NotFound(ErrorResponse.From(e));
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanApi planApi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    planApi.Id = 0;
                    var convertedPlan = planApi.ToPlan();
                    convertedPlan.InterestedUsers = planApi.InterestedUsers.Select(i => new User(i)).ToList();
                    var createdPlan = ConnectionDB.PlansModule.DataPlan.Save(convertedPlan);
                    if (createdPlan != null)
                    {
                        var uri = Url.Action("Create", planApi.Id);
                        return Created(uri, planApi);
                    }
                }
                return BadRequest(ErrorResponse.FromModelState(ModelState));
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanApi planApi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (planApi.Id <= 0) { return BadRequest($"The plan's id is required or is invalid: {planApi.Id}"); }
                    var convertedPlan = planApi.ToPlan();
                    var updatedPlan = ConnectionDB.PlansModule.DataPlan.Save(convertedPlan);
                    if (updatedPlan != null) { return Ok(); }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var planFlag = ConnectionDB.PlansModule.DataPlan.Delete(id);
                return NoContent();
            }
            catch(Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }
    }
}