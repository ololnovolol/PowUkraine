using System;

namespace IdentityServer.Data
{
    public class DbInitializer

    {
        public static void Initialize(AuthorizationDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

        }


    }
}
