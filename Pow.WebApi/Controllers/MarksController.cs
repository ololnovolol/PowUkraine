using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Services.Interfaces;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IBLLMarkService _markService;

        public MarksController(IBLLMarkService markService)
        {
            _markService = markService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            var marks = await _markService.GetAll();
            return Ok(marks);
        }
    }
}
