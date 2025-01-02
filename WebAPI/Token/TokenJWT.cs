using System.IdentityModel.Tokens.Jwt;

namespace WebAPI.Token
{
    public class TokenJWT
    {
        private JwtSecurityToken _token;

        internal TokenJWT(JwtSecurityToken token) 
        {
            _token = token;
        }

        public string Value => new JwtSecurityTokenHandler().WriteToken(_token);
    }
}
