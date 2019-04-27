using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Api.Models;

namespace Plans.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase, IServicesApi<UserApi>
    {
        public ISet<int> CacheIds { get; }

        [HttpPost]
        public IActionResult Create([FromBody] UserApi objApi)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult List()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public IActionResult Remove(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserApi objApi)
        {
            throw new NotImplementedException();
        }
    }
}