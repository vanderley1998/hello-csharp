using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Api.Models.Extensions;

namespace Plans.Api.Controllers.Plan
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InnerPlanController : ControllerBase
    {

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var list = ConnectionDB.PlansModule.DataInnerPlan.GetById(id).ToList();
                return Ok(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }

    }
}