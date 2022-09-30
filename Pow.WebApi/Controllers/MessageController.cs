using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pow.WebApi.Controllers.Base;

namespace Pow.WebApi.Controllers
{
    public class MessageController : BaseController
    {
        //[Authorize(Policy = "UserAccess")]
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Users_only___User___get method=)");
        }

        //[Authorize(Policy = "AdminAccess")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Admin Access_only___Admin___getAll");
        }


        [HttpPost]
        public IActionResult Message([FromQuery]string json)
        {
            Console.WriteLine(json);
            return Ok();
        }

        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok($"delete{id}");
        }
    }
}