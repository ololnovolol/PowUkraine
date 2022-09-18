using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pow.WebApi.Controllers.Base;
using System;

namespace Pow.WebApi.Controllers
{
    public class HomeController : BaseController
    {
        [Authorize(Policy = "UserAccess")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok($"Users_only___User___get method=)");
        }

        [Authorize(Policy = "AdminAccess")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Admin Access_only___Admin___getAll");
        }

        [Authorize(Policy = "UserAccess")]
        [HttpPost]
        public IActionResult Create([FromBody] string model)
        {
            return Ok(model);
        }

        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok($"delete{id}");
        }
    }
}
