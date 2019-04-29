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
    public class UserController : ControllerBase, IServicesApi<UserApi>
    {
        public ISet<int> CacheIds { get; }

        public UserController()
        {
            CacheIds = ConnectionDB.PlansModule.DataUser.GetAll().Select(u => u.Id).ToHashSet();
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserApi user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = 0;
                    var convertedUser = user.ToUser();
                    convertedUser.LastchangedDate = convertedUser.RegisterDate;
                    var createdUser = ConnectionDB.PlansModule.DataUser.Save(convertedUser);
                    if (createdUser != null)
                    {
                        CacheIds.Add(createdUser.Id);
                        //var uri = Url.Action("Plan", new { id = createdPlan.Id }); <--- TIRAR DÚVIDA PQ NÃO FUNCIONA. RETORNANDO VÁZIO!
                        //return Created(uri, plan);
                        return Ok(new { createdUser.Id });
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var idUser = CacheIds.First(u => u == id);
                var user = ConnectionDB.PlansModule.DataUser.Get(idUser).ToUserApi();
                return Ok(user);
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no user with id = {id}");
            }
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
                var idUser = CacheIds.First(u => u == id);
                var userFlag = ConnectionDB.PlansModule.DataUser.Delete(idUser);
                if (userFlag) { CacheIds.Remove(idUser); }
                return NoContent();
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
                return NotFound($"There's no user with id = {id}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError); // <--- PERGUNTAR QUAL A MELHOR FORMA DE EXPOR SQL EXCEPTION
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
                    var idUser = CacheIds.First(u => u == userApi.Id);
                    var convertedUser = userApi.ToUser();
                    var updatedUser = ConnectionDB.PlansModule.DataUser.Save(convertedUser);
                    if (convertedUser != null)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
            }
            catch (InvalidOperationException)
            {
                return NotFound($"There's no user with id = {userApi.Id}");
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