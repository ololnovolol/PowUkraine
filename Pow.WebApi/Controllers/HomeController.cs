using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;
using Pow.Infrastructure.Repositories.Interfaces;
using Pow.WebApi.Controllers.Base;
using Pow.WebApi.Extensions;
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

        [Authorize(Policy = "UserAccess")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) => Ok($"delete{id}");
    }
}
