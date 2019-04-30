using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Plans.Api.Controllers.View
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TotalPlanByUserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var list = ConnectionDB.PlansModule.ViewTotalPlansByUser.GetAll().ToList();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var list = ConnectionDB.PlansModule.ViewTotalPlansByUser.Get(id);
            return Ok(list);
        }
    }
}