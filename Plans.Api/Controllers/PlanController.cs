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

namespace Plans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase, IServicesApi<PlanApi>
    {

        public ISet<int> CacheIds { get; }

        public PlanController()
        {
            CacheIds = ConnectionDB.PlansModule.DataPlan.GetAll().Select(p => p.Id).ToHashSet();
        }

        [HttpGet]
        public IActionResult List()
        {
            var list = ConnectionDB.PlansModule.DataPlan.GetAll().Select(p => p.ToPlanApi()).ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var idPlan = CacheIds.First(p => p == id);
                var plan = ConnectionDB.PlansModule.DataPlan.Get(idPlan).ToPlanApi();
                return Ok(plan);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan with id = {id}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanApi plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    plan.Id = 0;
                    var convertedPlan = plan.ToPlan();
                    var createdPlan = ConnectionDB.PlansModule.DataPlan.Save(convertedPlan);
                    if (createdPlan != null)
                    {
                        CacheIds.Add(createdPlan.Id);
                        //var uri = Url.Action("Plan", new { id = createdPlan.Id }); <--- TIRAR DÚVIDA PQ NÃO FUNCIONA. RETORNANDO VÁZIO!
                        //return Created(uri, plan);
                        return Ok(new { createdPlan.Id });
                    }
                }
                return BadRequest("The plan object received isn't valid");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
            }
            
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanApi plan)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (plan.Id <= 0) { return BadRequest($"The plan's id is required or is invalid: {plan.Id}"); }
                    var idPlan = CacheIds.First(p => p == plan.Id);
                    var convertedPlan = plan.ToPlan();
                    var updatedPlan = ConnectionDB.PlansModule.DataPlan.Save(convertedPlan);
                    if (updatedPlan != null)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan with id = {plan.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var idPlan = CacheIds.First(p => p == id);
                var planFlag = ConnectionDB.PlansModule.DataPlan.Delete(idPlan);
                if (planFlag) { CacheIds.Remove(idPlan); }
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return NotFound($"There's no plan with id = {id}");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
            }
        }

    }
}