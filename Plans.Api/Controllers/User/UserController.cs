using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Api.Models;
using Plans.Api.Models.Extensions;

namespace Plans.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create([FromBody] UserApi userApi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userApi.Id = 0;
                    var convertedUser = userApi.ToUser();
                    convertedUser.RegisterDate = DateTime.Now;
                    convertedUser.LastchangedDate = convertedUser.RegisterDate;
                    var createdUser = ConnectionDB.PlansModule.DataUser.Save(convertedUser);
                    if (createdUser != null) { return Ok(new { createdUser.Id }); }
                }
                return BadRequest("The plan object received isn't valid");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = ConnectionDB.PlansModule.DataUser.Get(id).ToUserApi();
            if(user != null) { return Ok(user); }
            return NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            var list = ConnectionDB.PlansModule.DataUser.GetAll().Select(u => u.ToUserApi()).ToList();
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var userFlag = ConnectionDB.PlansModule.DataUser.Delete(id);
                if (userFlag)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserApi userApi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userApi.Id <= 0) { return BadRequest($"The user's id is required or is invalid: {userApi.Id}"); }
                    var convertedUser = userApi.ToUser();
                    var updatedUser = ConnectionDB.PlansModule.DataUser.Save(convertedUser);
                    if (convertedUser != null) { return Ok(); }
                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}