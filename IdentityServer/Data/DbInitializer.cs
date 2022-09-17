using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Data
{
    // todo remove
    public class DbInitializer
    {
        public static void Initialize(AuthorizationDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

        }

        
    }
}
