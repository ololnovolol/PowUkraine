using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.WebApi.Controllers.Base;
using Pow.WebApi.Models;

namespace Pow.WebApi.Controllers
{
    public class HomeController : BaseController
    {
        // [Authorize(Policy = "UserAccess")]
        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Get() => Ok("Users_only___User___get method=)");

        // [Authorize(Policy = "AdminAccess")]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll() => Ok("Admin Access_only___Admin___getAll");

        [HttpPost]
        public IActionResult Message(IFormCollection data, IFormFile imageFile)
        {
            MessageVm msg = new()
            {
                Title = data["PhoneNumber"],
                PhoneNumber = data["PhoneNumber"],
                Data = DateTime.Parse(data["Data"]),
                Description = data["Description"],
                Attachment = data["Attachment"],
            };

            return Ok("goood!");
        }

        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) => Ok($"delete{id}");
    }
}
