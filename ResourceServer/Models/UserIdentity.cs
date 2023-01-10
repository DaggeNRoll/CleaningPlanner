using Microsoft.AspNetCore.Identity;

namespace ResourceServer.Models
{
    public class UserIdentity : IdentityUser
    {
        public int UserDbId { get; set; }
    }
}
