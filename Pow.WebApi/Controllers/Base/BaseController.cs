using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Pow.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        internal Guid UserId
            => User.Identity is { IsAuthenticated: false }
                ? Guid.Empty
                : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
    }
}
