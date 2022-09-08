using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models
{
    public class AppUser : IdentityUser
    {
        // todo expand
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
