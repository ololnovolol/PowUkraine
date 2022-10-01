using System;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Pow.Application.Interfaces;

namespace Pow.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                Debug.Assert(_httpContextAccessor != null, nameof(_httpContextAccessor) + " != null");

                var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
            }
        }
    }
}
