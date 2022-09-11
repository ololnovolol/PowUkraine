namespace IdentityServer.Data
{
    // todo remove
    public class DbInitializer
    {
        public static void Initialize(AuthorizationDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
