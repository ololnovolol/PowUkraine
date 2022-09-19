using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class DbInitializer

    {
        public static void Initialize(AuthorizationDbContext context)
        {

            context.Database.Migrate();
            context.Database.EnsureCreated();

        }


    }
}
