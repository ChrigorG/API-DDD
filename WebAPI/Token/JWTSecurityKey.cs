using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI.Token
{
    public class JWTSecurityKey
    {
        public static SymmetricSecurityKey Create(string key)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
    }
}
