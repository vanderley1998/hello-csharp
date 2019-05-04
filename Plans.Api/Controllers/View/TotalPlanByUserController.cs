using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Plans.Api.Controllers.View
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TotalPlanByUserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var list = ConnectionDB.PlansModule.ViewTotalPlansByUser.GetAll().ToList();
                return Ok(list);
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
                var list = ConnectionDB.PlansModule.ViewTotalPlansByUser.Get(id);
                return Ok(list);
            }
            catch (Exception e)
            {
                ErrorResponse errorResponse = ErrorResponse.From(e);
                return StatusCode(500, errorResponse);
            }
        }
    }
}