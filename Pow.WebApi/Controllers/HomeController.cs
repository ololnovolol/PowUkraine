using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pow.WebApi.Controllers.Base;
using System;

namespace Pow.WebApi.Controllers
{
    public class HomeController : BaseController
    {
        //[Authorize(Roles ="User")]
        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get()
        {
            return Ok($"Users_only______get=)");
        }

        //[Authorize(Roles ="User")]
        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Admin_only____getAll");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] string model)
        {
            return Ok(model);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok($"delete{id}");
        }
    }
}
