using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plans.Api.Controllers
{
    interface IServicesApi<TObjApi>
    {
        ISet<int> CacheIds { get; }

        [HttpGet]
        IActionResult List();

        [HttpGet("{id}")]
        IActionResult Get(int id);

        [HttpPost]
        IActionResult Create([FromBody] TObjApi objApi);

        [HttpPut]
        IActionResult Update([FromBody] TObjApi objApi);

        [HttpDelete("{id}")]
        IActionResult Remove(int id);
    }
}
