using System;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.WebApi.Controllers.Base;

namespace Pow.WebApi.Controllers
{
    public class HomeController : BaseController
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
        public IActionResult Message(IFormCollection data, IFormFile imageFile)
        {
            var msg = new Models.MessageVm();
            msg.Title = data["PhoneNumber"];
            msg.PhoneNumber = data["PhoneNumber"];
            msg.Data = DateTime.Parse(data["Data"]);
            msg.Description = data["Description"];
            msg.Attachment = data["Attachment"];


            return Ok("goood!");
        }



        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok($"delete{id}");
        }
    }
}