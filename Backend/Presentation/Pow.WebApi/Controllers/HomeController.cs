using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pow.WebApi.Controllers.Base;
using System;

namespace Pow.WebApi.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok($"get_{id}");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("getAll");
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
