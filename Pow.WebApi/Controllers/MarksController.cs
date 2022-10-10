using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pow.Application.Services.Interfaces;

namespace Pow.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IMarksOnMapService _marksOnMapService;

        public MarksController(IMarksOnMapService marksOnMapService)
        {
            _marksOnMapService = marksOnMapService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMarks()
        {
            var marksOnMap = await _marksOnMapService.GetAllAsync();

            return Ok(marksOnMap);
        }
    }
}
