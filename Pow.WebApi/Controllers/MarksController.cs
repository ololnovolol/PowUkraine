using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Models;
using Pow.Application.Services.Interfaces;

namespace Pow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IBLLMarkService _markService;

        public MarksController(IBLLMarkService markService) => _markService = markService;

        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            IEnumerable<MarkBL> marks = await _markService.GetAll();

            return Ok(marks);
        }
    }
}
