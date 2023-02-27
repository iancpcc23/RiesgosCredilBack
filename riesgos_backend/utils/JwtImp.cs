using backend.Interfaces;
using Microsoft.IdentityModel.Tokens;
using riesgos_backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace riesgos_backend.utils
{
    public class JwtImp : IJwt
    {
        private readonly IConfiguration _configuration;
        private readonly string _secretKeyJwt ;
        public JwtImp(IConfiguration configuration) { 
        _configuration= configuration;
         _secretKeyJwt = _configuration.GetSection("AppSettings:key_token").Value!;
        }
        public string createToken(UsuarioRiesgos payload)
        {
            List<Claim> claim = new List<Claim> { new Claim("username", payload.Usuario ),new Claim( JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKeyJwt));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken( claims:claim, expires: DateTime.Now.AddMinutes(30), signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return  jwt;
        }

    
        public dynamic verifyToken(string token)
        {
          
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKeyJwt);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var username = jwtToken.Claims.First(x => x.Type == "username").Value;
                // return account id from JWT token if validation successful
                return username;
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146233088)
                {
                    throw new Exception("El token ha expirado, inicie sesión nuevamente");

                }
                throw new Exception(ex.Message);
            }
        }
    }
}
