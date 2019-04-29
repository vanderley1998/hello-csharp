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
    public class PlanTypeController : ControllerBase, IServicesApi<PlanType>
    {
        public ISet<int> CacheIds { get; }

        public PlanTypeController()
        {
            CacheIds = ConnectionDB.PlansModule.DataPlanType.GetAll().Select(pt => pt.Id).ToHashSet();
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanType planType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    planType.Id = 0;
                    var createdPlanType = ConnectionDB.PlansModule.DataPlanType.Save(planType);
                    if (createdPlanType != null)
                    {
                        CacheIds.Add(createdPlanType.Id);
                        //var uri = Url.Action("Plan", new { id = createdPlan.Id }); <--- TIRAR DÚVIDA PQ NÃO FUNCIONA. RETORNANDO VÁZIO!
                        //return Created(uri, plan);
                        return Ok(new { createdPlanType.Id });
                    }
                }
                return BadRequest("The plan'type object received isn't valid");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var idPlanType = CacheIds.First(pt => pt == id);
                var planType = ConnectionDB.PlansModule.DataPlanType.Get(idPlanType);
                return Ok(planType);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan'type with id = {id}");
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
                var idPlanType = CacheIds.First(pt => pt == id);
                var planTypeFlag = ConnectionDB.PlansModule.DataPlanType.Delete(idPlanType);
                if (planTypeFlag) { CacheIds.Remove(idPlanType); }
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return NotFound($"There's no plan'type with id = {id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
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
                    var idPlanType = CacheIds.First(pt => pt == planType.Id);
                    var updatedPlanType = ConnectionDB.PlansModule.DataPlanType.Save(planType);
                    if (updatedPlanType != null)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no plan'type with id = {planType.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
            }
        }

        public IActionResult ListById(int id)
        {
            return NotFound();
        }
    }
}