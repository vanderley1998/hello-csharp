﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Plans.Api.Models.Extensions;

namespace Plans.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserHistoryController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult ListById(int id)
        {
            try
            {
                var list = ConnectionDB.PlansModule.DataUsersHistory.GetById(id).Select(ph => ph.ToUserHistoryApi()).ToList();
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