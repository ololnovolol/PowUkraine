using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Services;
using Pow.WebApi.Controllers.Base;
using Pow.WebApi.Extensions;
using Pow.WebApi.Models;

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
        /*[Authorize(Roles = "Admin")]*/
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Admin Access_only___Admin___getAll");
        }


        [HttpPost]
        public IActionResult Message(IFormCollection data, IFormFile imageFile)
        {
            var msg = new MessageModel();
            msg.Phone = data["PhoneNumber"];
            msg.EventDate = DateTime.Parse(data["Data"]);
            msg.Description = data["Description"];            
            msg.Email = data["Email"];
            
            var attachment = new AttachmentModel();
            attachment.Title = imageFile.FileName;
            attachment.File = imageFile.GetBytes().Result;

            

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