using Microsoft.AspNetCore.Identity;
using System;

namespace IdentityServer.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
