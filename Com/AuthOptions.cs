

using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Com
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurityKey { get => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));  }
    }
}