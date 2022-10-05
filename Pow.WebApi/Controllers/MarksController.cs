using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pow.Domain;
using Pow.Infrastructure.Repositories.Interfaces;

namespace Pow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MarksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            var marks = await _unitOfWork.Marks.GetAllAsync();
            return Ok(marks);
        }
    }
}
